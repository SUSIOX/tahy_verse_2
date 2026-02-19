import * as React from 'react';
import { useState, useCallback, useEffect } from 'react';
import {
    TahState,
    TAH_IDS,
    DEFAULT_STAGE_CONFIG,
    StageConfig,
    VectorLine,
    Point,
    Scene,
    LineStyle,
    TextLabel,
    DEFAULT_HOIST_POSITIONS,
    HoistRegistry
} from './types';
import StageCanvas, {
    PRINT_VIEWPORT_X,
    PRINT_VIEWPORT_Y,
    PRINT_VIEWPORT_W,
    PRINT_VIEWPORT_H
} from './components/StageCanvas';
import ControlPanel from './components/ControlPanel';
import LogPanel from './components/LogPanel';
import { motion, AnimatePresence } from 'framer-motion';
import {
    Maximize2,
    Minimize2,
    Settings,
    X,
    Info,
    Pencil,
    Eraser,
    Minus,
    MousePointer2,
    Plus,
    Save,
    Download,
    Upload,
    Type,
    Printer,
    TriangleAlert,
    ClipboardList,
    FileText
} from 'lucide-react';
import defaultSet from '../public/assets/default_set.json';

const App: React.FC = () => {
    // Helper to get initial tahy state
    const getInitialTahy = () => {
        const initial: Record<number, TahState> = {};
        TAH_IDS.forEach(id => {
            initial[id] = {
                id,
                dek: 0,
                uva: 0,
                pod: DEFAULT_STAGE_CONFIG.stageHeightCm,
                isHanging: false,
                isTopLimit: false,
                isBottomLimit: false,
                nosnost: 80, // Výchozí nosnost 80kg
                funkce: 'kulisy'
            };
        });
        return initial;
    };

    // Initial state with some test data
    const [tahy, setTahy] = useState<Record<number, TahState>>(getInitialTahy);

    // Scene Management State
    const [productionName, setProductionName] = useState(() => {
        const saved = localStorage.getItem('tahy-production-name');
        return saved || defaultSet.productionName || "Moje Inscenace";
    });
    const [activeSceneId, setActiveSceneId] = useState(() => {
        const saved = localStorage.getItem('tahy-active-scene-id');
        return saved || defaultSet.activeSceneId || "1";
    });
    const [scenes, setScenes] = useState<Scene[]>(() => {
        const saved = localStorage.getItem('tahy-scenes');
        if (saved) {
            try {
                return JSON.parse(saved);
            } catch (e) {
                console.error('Failed to load scenes from localStorage:', e);
            }
        }
        return (defaultSet.scenes as Scene[]) || [{ id: "1", name: "Scéna 1", tahy: getInitialTahy() }];
    });

    const [selectedTahId, setSelectedTahId] = useState<number>(TAH_IDS[0]);
    const [logs, setLogs] = useState<string[]>([]);
    const [isSidebarOpen, setIsSidebarOpen] = useState(true);
    const [stageConfig, setStageConfig] = useState<StageConfig>(() => {
        try {
            const saved = localStorage.getItem('tahy-stage-config');
            let config: StageConfig;
            if (saved) {
                config = JSON.parse(saved);
            } else {
                config = { ...DEFAULT_STAGE_CONFIG, ...(defaultSet.stageConfig as any) };
            }
            // VŽDY přepočítat měřítko při startu pro jistotu (Scale = Delta Px / Delta Cm)
            const distancePx = config.zeroLevelY - config.topLimitY;
            config.scale = distancePx / config.stageHeightCm;
            return config;
        } catch (e) {
            console.error('Failed to load stage config', e);
            return DEFAULT_STAGE_CONFIG;
        }
    });

    // Persist stageConfig whenever it changes
    useEffect(() => {
        localStorage.setItem('tahy-stage-config', JSON.stringify(stageConfig));
    }, [stageConfig]);

    // Initialize hoistPositions from localStorage or default
    const [hoistPositions, setHoistPositions] = useState<HoistRegistry>(() => {
        try {
            const saved = localStorage.getItem('tahy-hoist-positions');
            if (saved) {
                const parsed = JSON.parse(saved);
                // Nastavení automatické opravy předchozích chybných souřadnic z cache
                let changed = false;
                if (parsed[4] && parsed[4].x === 789.2) { parsed[4].x = 744; changed = true; }
                if (parsed[11] && parsed[11].x === 999.2) { parsed[11].x = 846; changed = true; }
                if (parsed[16] && parsed[16].x === 1149.2) { parsed[16].x = 937; changed = true; }

                if (changed) {
                    localStorage.setItem('tahy-hoist-positions', JSON.stringify(parsed));
                }
                return parsed;
            }
            return (defaultSet.hoistPositions as HoistRegistry) || DEFAULT_HOIST_POSITIONS;
        } catch (e) {
            console.error('Failed to load hoist positions', e);
            return DEFAULT_HOIST_POSITIONS;
        }
    });
    // Persist hoistPositions whenever they change
    useEffect(() => {
        localStorage.setItem('tahy-hoist-positions', JSON.stringify(hoistPositions));
    }, [hoistPositions]);

    const [isPositioningMode, setIsPositioningMode] = useState(false);
    const [previousSceneId, setPreviousSceneId] = useState<string | null>(null);
    const [zoomLevel, setZoomLevel] = useState(1);
    const [isHoistConfigMode, setIsHoistConfigMode] = useState(false);
    const [isSettingsOpen, setIsSettingsOpen] = useState(false);
    const [focusedPixelValue, setFocusedPixelValue] = useState<number | null>(null);

    // Virtuální Scéna 0 pro režim pozicování (vše nulové)
    const SCENE_ZERO: Scene = React.useMemo(() => {
        // Technické parametry bereme z první scény, abychom je v S0 viděli a mohli měnit
        const techSource = scenes[0]?.tahy;
        return {
            id: "0",
            name: "Scéna 0 (Prázdná)",
            tahy: TAH_IDS.reduce((acc, id) => {
                const isLight = techSource?.[id]?.funkce === 'světla';
                acc[id] = {
                    id,
                    dek: 0,
                    uva: 0,
                    pod: isLight ? (techSource?.[id]?.pod ?? 0) : 0,
                    isHanging: isLight ? true : false,
                    isTopLimit: false,
                    isBottomLimit: false,
                    nosnost: techSource?.[id]?.nosnost ?? 80,
                    funkce: techSource?.[id]?.funkce ?? 'kulisy'
                };
                return acc;
            }, {} as Record<number, TahState>),
            vectorLines: [],
            textLabels: []
        };
    }, [scenes]);

    const displayScenes = React.useMemo(() =>
        isPositioningMode ? [SCENE_ZERO, ...scenes] : scenes
        , [isPositioningMode, scenes, SCENE_ZERO]);

    // Save scenes to localStorage whenever they change
    useEffect(() => {
        localStorage.setItem('tahy-scenes', JSON.stringify(scenes));
    }, [scenes]);

    // Save active scene ID to localStorage
    useEffect(() => {
        localStorage.setItem('tahy-active-scene-id', activeSceneId);
    }, [activeSceneId]);

    // Save production name to localStorage
    useEffect(() => {
        localStorage.setItem('tahy-production-name', productionName);
    }, [productionName]);

    // Initialize state from the active scene when component mounts or scenes change
    useEffect(() => {
        const activeScene = displayScenes.find(s => s.id === activeSceneId);
        if (activeScene) {
            setTahy(activeScene.tahy);
            setVectorLines(activeScene.vectorLines || []);
            setTextLabels(activeScene.textLabels || []);
        }
    }, [activeSceneId, displayScenes]);

    // Sync current state to active scene in scenes array
    const syncCurrentStateToScenes = useCallback((newTahy?: Record<number, TahState>, newVectors?: VectorLine[], newLabels?: TextLabel[]) => {
        if (activeSceneId === "0") {
            // Propagace technických parametrů do všech scén
            if (newTahy) {
                setScenes(prevScenes => prevScenes.map(s => ({
                    ...s,
                    tahy: Object.keys(s.tahy).reduce((acc, idStr) => {
                        const id = Number(idStr);
                        const isLight = newTahy[id].funkce === 'světla';
                        acc[id] = {
                            ...s.tahy[id],
                            nosnost: newTahy[id].nosnost,
                            funkce: newTahy[id].funkce,
                            // Pozici od země u světel považujeme za technický parametr (rigging)
                            pod: isLight ? newTahy[id].pod : s.tahy[id].pod,
                            isHanging: isLight ? true : s.tahy[id].isHanging
                        };
                        return acc;
                    }, {} as Record<number, TahState>)
                })));
            }
            return;
        }
        setScenes(prevScenes => prevScenes.map(s => {
            if (s.id === activeSceneId) {
                return {
                    ...s,
                    tahy: newTahy || s.tahy,
                    vectorLines: newVectors || s.vectorLines || [],
                    textLabels: newLabels || s.textLabels || []
                };
            }
            return s;
        }));
    }, [activeSceneId]);

    const updateStageConfig = useCallback((updates: Partial<StageConfig>) => {
        setStageConfig((prev: StageConfig) => {
            const next = { ...prev, ...updates };
            // Scale = Delta Px / Delta Cm
            const distancePx = next.zeroLevelY - next.topLimitY;
            next.scale = distancePx / next.stageHeightCm;
            return next;
        });
    }, []);

    const handleImageUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (e) => {
                const result = e.target?.result as string;
                updateStageConfig({ customBgImage: result });
                addLog('Podkladový obrázek byl úspěšně změněn');
            };
            reader.readAsDataURL(file);
        }
    };

    const resetBackgroundImage = () => {
        updateStageConfig({ customBgImage: undefined });
        addLog('Podkladový obrázek byl resetován na výchozí');
    };

    // Force update topLimitY if it's still the old default
    useEffect(() => {
        if (stageConfig.topLimitY === 32.98 || stageConfig.topLimitY === 43 || stageConfig.topLimitY === 124.9) {
            updateStageConfig({ topLimitY: 120.9 });
        }
    }, [stageConfig.topLimitY, updateStageConfig]);

    const [brushColor, setBrushColor] = useState('#000000'); // Default to black
    const [drawTool, setDrawTool] = useState<'pen' | 'eraser' | 'line' | 'select' | 'text'>('pen');
    const [isDrawingMode, setIsDrawingMode] = useState(false);
    const [vectorLines, setVectorLines] = useState<VectorLine[]>([]);
    const [selectedVectorId, setSelectedVectorId] = useState<string | null>(null);
    const [editingLineId, setEditingLineId] = useState<string | null>(null);
    const [defaultLineStyle, setDefaultLineStyle] = useState<LineStyle>('dashed');
    const [defaultLineWidth, setDefaultLineWidth] = useState<number>(1);

    const [textLabels, setTextLabels] = useState<TextLabel[]>([]);
    const [selectedTextId, setSelectedTextId] = useState<string | null>(null);
    const [editingTextId, setEditingTextId] = useState<string | null>(null);
    const handleUpdateTextLabels = useCallback((next: TextLabel[]) => {
        setTextLabels(next);
        syncCurrentStateToScenes(undefined, undefined, next);
    }, [syncCurrentStateToScenes]);

    const updateTextLabel = (id: string, updates: Partial<TextLabel>) => {
        setTextLabels(prev => {
            const next = prev.map(l => l.id === id ? { ...l, ...updates } : l);
            syncCurrentStateToScenes(undefined, undefined, next);
            return next;
        });
    };

    const removeTextLabel = (id: string) => {
        setTextLabels(prev => {
            const next = prev.filter(l => l.id !== id);
            syncCurrentStateToScenes(undefined, undefined, next);
            return next;
        });
        if (selectedTextId === id) setSelectedTextId(null);
        if (editingTextId === id) setEditingTextId(null);
    };

    const handleTogglePositioningMode = () => {
        if (isPositioningMode) {
            // Exit mode
            setIsPositioningMode(false);
            setZoomLevel(1); // Reset zoom when exiting
            if (previousSceneId) {
                setActiveSceneId(previousSceneId);
            }
            addLog('Ukončeno ruční pozicování tahů - Pozice uloženy');
        } else {
            // Enter mode
            setPreviousSceneId(activeSceneId);
            setIsPositioningMode(true);
            setActiveSceneId("0");
            setIsSettingsOpen(false); // Close settings modal when starting
            setSelectedTahId(-1); // Deselect everything
            setSelectedTextId(null);
            setSelectedVectorId(null);
            setIsSidebarOpen(true); // Ensure sidebar is open to show the toggle button
            addLog('Spuštěno ruční pozicování tahů - Aktivní Scéna 0');
        }
    };

    const handleUpdateVectorLines = useCallback((next: VectorLine[]) => {
        setVectorLines(next);
        syncCurrentStateToScenes(undefined, next, undefined);
    }, [syncCurrentStateToScenes]);

    const updateVectorLine = (id: string, updates: Partial<VectorLine>) => {
        if (id === 'default') {
            if (updates.lineStyle) setDefaultLineStyle(updates.lineStyle);
            if (updates.lineWidth) setDefaultLineWidth(updates.lineWidth);
            if (updates.color) setBrushColor(updates.color);
            return;
        }
        setVectorLines(prev => {
            const next = prev.map(l => l.id === id ? { ...l, ...updates } : l);
            syncCurrentStateToScenes(undefined, next, undefined);
            return next;
        });
    };

    const handleColorChange = (newColor: string) => {
        setBrushColor(newColor);
        if (selectedVectorId) {
            setVectorLines(prev => prev.map(line =>
                line.id === selectedVectorId ? { ...line, color: newColor } : line
            ));
        } else if (selectedTahId !== -1) {
            updateTah(selectedTahId, { color: newColor });
        }
    };

    const addLog = useCallback((msg: string) => {
        const time = new Date().toLocaleTimeString();
        setLogs(prev => [`[${time}] ${msg}`, ...prev].slice(0, 50));
    }, []);

    // Scene Management Helpers
    const addScene = () => {
        const newSceneId = Date.now().toString();
        // Create new scene as a copy of the current state
        const newScene: Scene = {
            id: newSceneId,
            name: `Scéna ${scenes.length + 1}`,
            tahy: JSON.parse(JSON.stringify(tahy))
        };
        setScenes(prev => [...prev, newScene]);
        setActiveSceneId(newSceneId);
        addLog(`Vytvořena nová scéna: Scéna ${scenes.length + 1}`);
    };

    const switchScene = (sceneId: string) => {
        const scene = displayScenes.find(s => s.id === sceneId);
        if (scene) {
            setActiveSceneId(sceneId);
            setTahy(scene.tahy);
            if (scene.vectorLines) setVectorLines(scene.vectorLines);
            if (scene.textLabels) setTextLabels(scene.textLabels);
            addLog(`Přepnuto na scénu: ${scene.name}`);
        }
    };

    const handleSceneNameChange = (id: string, newName: string) => {
        if (id === "0") return; // Scéna 0 se nepřejmenovává
        setScenes(prev => prev.map(s => s.id === id ? { ...s, name: newName } : s));
    };

    const exportScenes = () => {
        const data = {
            productionName,
            scenes,
            activeSceneId,
            stageConfig,
            hoistPositions,
            exportDate: new Date().toISOString()
        };
        const blob = new Blob([JSON.stringify(data, null, 2)], { type: 'application/json' });
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `tahy-${productionName.replace(/\s+/g, '-').toLowerCase()}-${new Date().toISOString().split('T')[0]}.json`;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        URL.revokeObjectURL(url);
        addLog(`Scény exportovány: ${scenes.length} scén`);
    };

    const importScenes = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (e) => {
                try {
                    const data = JSON.parse(e.target?.result as string);
                    if (data.scenes && Array.isArray(data.scenes)) {
                        setScenes(data.scenes);
                        setProductionName(data.productionName || 'Importovaná inscenace');
                        setActiveSceneId(data.activeSceneId || data.scenes[0]?.id || '1');

                        if (data.stageConfig) {
                            setStageConfig(data.stageConfig);
                        }
                        if (data.hoistPositions) {
                            setHoistPositions(data.hoistPositions);
                        }

                        addLog(`Scény a konfigurace importovány: ${data.scenes.length} scén`);
                    } else {
                        alert('Neplatný formát souboru');
                    }
                } catch (error) {
                    alert('Chyba při načítání souboru: ' + error);
                }
            };
            reader.readAsText(file);
        }
        // Reset file input
        event.target.value = '';
    };

    const generateSceneSummary = () => {
        const activeScene = scenes.find(s => s.id === activeSceneId);
        if (!activeScene) return;

        const usedHoists: string[] = [];

        TAH_IDS.forEach(id => {
            const tah = activeScene.tahy[id];
            if (!tah) return;

            // Do soupisu zahrň pouze tahy, které mají zavěšenou dekoraci
            if (tah.isHanging) {
                // Formát: ID | výška | rozměr | úvazek | název
                const totalHeight = tah.pod + tah.dek + tah.uva;
                let line = `<strong>TAH ${id}</strong>: ${totalHeight}cm`;

                if (tah.dek > 0) {
                    line += ` (${tah.dek}cm)`;
                }

                if (tah.uva > 0) {
                    line += ` <span style="color: red; font-weight: bold;">${tah.uva}cm</span>`;
                }

                if (tah.name) {
                    line += ` - <span style="color: #3b82f6; font-weight: bold;">${tah.name}</span>`;
                }

                usedHoists.push(line);
            }
        });

        if (usedHoists.length === 0) {
            addLog('Žádné tahy k zobrazení v soupisu');
            return;
        }

        // Vytvoř textový soupis s HTML formátováním (bez divů, pouze span a br)
        const summaryText = `<span style="font-family: monospace; font-weight: 900; font-size: 18px; color: #000;">SOUPIS TAHŮ</span><br/>` +
            `<span style="font-family: monospace; font-size: 10px; color: #888; font-weight: bold; text-transform: uppercase;">výška rozměr úvazek název</span><br/>` +
            `<span style="font-family: monospace; font-size: 13px;">${usedHoists.join('<br/>')}</span>`;

        // Přidej textové pole pro soupis (50px od kraje, pod názvem)
        const summaryLabel: TextLabel = {
            id: `summary-${Date.now()}-${Math.random().toString(36).substr(2, 9)}`,
            pos: { x: 50, y: 150 },
            text: summaryText,
            color: '#000000',
            fontSize: 14,
            width: 300,
            backgroundColor: '#ffffff',
            backgroundOpacity: 0.9
        };

        // Přidej textové pole pro název inscenace a scény do levého horního rohu (x: 50, y: 50)
        const productionLabel: TextLabel = {
            id: `production-${Date.now()}-${Math.random().toString(36).substr(2, 9)}`,
            pos: { x: 50, y: 50 },
            text: `<span style="font-weight: 900; font-size: 24px;">${productionName}</span> <span style="font-weight: 700; font-size: 20px; color: #000;">- ${activeScene.name}</span>`,
            color: '#000000',
            fontSize: 24,
            backgroundColor: '#ffffff',
            backgroundOpacity: 0.9
        };

        const newLabels = [...textLabels, summaryLabel, productionLabel];
        setTextLabels(newLabels);
        syncCurrentStateToScenes(undefined, undefined, newLabels);
        setSelectedTextId(null); // Ukončit editaci textových polí
        setSelectedVectorId(null);
        setSelectedTahId(-1);
        addLog(`Soupis tahů a název inscenace vytvořeny pro scénu: ${activeScene.name}`);
    };


    const updateTah = useCallback((id: number, updatesOrFn: Partial<TahState> | ((prev: TahState) => Partial<TahState>)) => {
        const currentTah = tahy[id];
        const updates = typeof updatesOrFn === 'function' ? updatesOrFn(currentTah) : updatesOrFn;
        const newTah = { ...currentTah, ...updates };
        const nextTahy = { ...tahy, [id]: newTah };

        setTahy(nextTahy);
        syncCurrentStateToScenes(nextTahy);
    }, [tahy, syncCurrentStateToScenes]);

    const selectedTah = tahy[selectedTahId];

    return (
        <div className="flex h-screen bg-[#050505] text-zinc-100 font-sans selection:bg-blue-500/30 overflow-hidden">
            <AnimatePresence mode="wait">
                {isSidebarOpen && (
                    <motion.aside
                        initial={{ width: 0, opacity: 0 }}
                        animate={{ width: 400, opacity: 1 }}
                        exit={{ width: 0, opacity: 0 }}
                        className="h-full flex flex-col z-40 overflow-hidden relative border-r border-zinc-800/50 bg-zinc-900/50 backdrop-blur-xl shadow-2xl flex-shrink-0 no-print transition-all duration-500"
                        style={{ filter: isPositioningMode ? 'blur(0px)' : 'none' }} // Sidebar itself is not blurred, but content inside might be selectively handled, or we blur everything ELSE. 
                    // Actually user request: "vsechno krome obrazku a icony menu je blure"
                    // But the triangle icon is IN the menu (ControlPanel). So we should probably keep the ControlPanel visible but maybe blur other parts?
                    // Or better: The user said "menu zmizi" (disappears) BUT "active triangle... everything except image and menu icon is blurred".
                    // "jak budu hotov zase kliknu na trojuhelnik v menu configu".
                    // This implies the menu MUST remain visible OR the triangle button moves/stays visible. 
                    // Implementation: We will blur the *contents* of the sidebar except the button, or just blur the main UI overlays.
                    >
                        <div className={`p-8 border-b border-zinc-800/50 bg-zinc-900/50 transition-all duration-500 ${isPositioningMode ? 'opacity-20 blur-sm pointer-events-none' : ''}`}>
                            <div className="flex items-center justify-between mb-8">
                                <div className="flex items-center gap-3">
                                    <div className="w-10 h-10 bg-blue-600 rounded-2xl flex items-center justify-center shadow-lg shadow-blue-600/20 rotate-3 group-hover:rotate-0 transition-transform">
                                        <div className="w-5 h-5 border-2 border-white rounded-sm" />
                                    </div>
                                    <div>
                                        <h1 className="text-xl font-black tracking-tighter uppercase leading-none">Tahy Jirka</h1>
                                        <span className="text-[10px] text-zinc-500 font-bold tracking-[0.2em] uppercase">Verze 2.0</span>
                                    </div>
                                </div>
                                <button
                                    onClick={() => setIsSidebarOpen(false)}
                                    className="p-3 hover:bg-zinc-800/50 rounded-2xl text-zinc-400 transition-all active:scale-95 group"
                                >
                                    <Minimize2 className="w-5 h-5 group-hover:text-blue-400" />
                                </button>
                            </div>
                        </div>

                        <div className="flex-1 overflow-y-auto custom-scrollbar bg-zinc-900/20 relative">
                            {/* Blur Overlay for Sidebar Content when in Positioning Mode, excluding the Control Panel top part? 
                                Actually, the button is INSIDE ControlPanel. We need to pass the "blur mode" down to ControlPanel 
                                so it can blur everything EXCEPT the toggle button. 
                                OR easier: we just accept that the whole sidebar stays visible for now, or we style ControlPanel carefully.
                                Let's try to blur the rest of the app.*/}
                            <div className="p-8 space-y-8">
                                <ControlPanel
                                    tahy={tahy}
                                    selectedId={selectedTahId}
                                    onSelectId={setSelectedTahId}
                                    tah={selectedTah}
                                    onUpdate={updateTah}
                                    onHang={(id, dek, uva, pod) => {
                                        // Default values: 30cm sling, 50cm decoration, positioned at half stage height
                                        const defaultUva = 30;
                                        const defaultDek = 50;
                                        const defaultPod = Math.round((stageConfig.stageHeightCm / 2) - defaultDek - defaultUva);

                                        updateTah(id, {
                                            dek: defaultDek,
                                            uva: defaultUva,
                                            pod: Math.max(0, defaultPod),
                                            isHanging: true
                                        });
                                        addLog(`Tah ${id}: Dekorace zavěšena (${defaultDek}cm + ${defaultUva}cm) ve výšce ${Math.max(0, defaultPod)}cm`);
                                    }}
                                    onClearAll={() => {
                                        const reset: Record<number, TahState> = {};
                                        TAH_IDS.forEach(id => {
                                            reset[id] = { id, dek: 0, uva: 0, pod: 0, isHanging: false, isTopLimit: false, isBottomLimit: false };
                                        });
                                        setTahy(reset);
                                        addLog("Všechny tahy byly vymazány");
                                    }}
                                    onResetToDefault={() => {
                                        // Reset to SCENE_ZERO logic but keeping current technical params (which are handled by sync, but here we reset active values)
                                        // Actually SCENE_ZERO is dynamic now. So we can just copy it.
                                        setTahy(SCENE_ZERO.tahy);
                                        addLog("Tahy byly nastaveny na výchozí hodnoty");
                                    }}
                                    onAddLog={addLog}
                                    stageConfig={stageConfig}
                                    onUpdateConfig={setStageConfig}
                                    onTogglePositioningMode={handleTogglePositioningMode}
                                    onUpdateHoistPositions={setHoistPositions}
                                    isPositioningMode={isPositioningMode}
                                    hoistPositions={hoistPositions}
                                />

                                {/* Production and Scene Management Panel - Blurred in positioning mode */}
                                <div className={`bg-zinc-800/30 border border-zinc-700/50 rounded-2xl p-6 space-y-6 transition-all duration-500 ${isPositioningMode ? 'opacity-20 blur-sm pointer-events-none' : ''}`}>
                                    <div className="space-y-4">
                                        <h3 className="text-sm font-black text-zinc-300 uppercase tracking-wider">Inscenace</h3>
                                        <input
                                            value={productionName}
                                            onChange={(e) => setProductionName(e.target.value)}
                                            className="w-full h-10 px-4 bg-zinc-900/50 border border-zinc-700/50 rounded-xl text-zinc-100 font-bold transition-all outline-none focus:border-blue-500/50 focus:bg-zinc-800 placeholder-zinc-600"
                                            placeholder="NÁZEV INSCENACE"
                                        />
                                    </div>

                                    <div className="space-y-4">
                                        <div className="flex items-center justify-between">
                                            <h4 className="text-xs font-black text-zinc-400 uppercase tracking-wider">Scény</h4>
                                            <button
                                                onClick={addScene}
                                                className="p-2 bg-blue-600/20 hover:bg-blue-600/30 border border-blue-500/30 rounded-xl text-blue-400 hover:text-blue-300 transition-all active:scale-95"
                                                title="Přidat novou scénu"
                                            >
                                                <Plus className="w-4 h-4" />
                                            </button>
                                        </div>

                                        <div className="space-y-3">
                                            <div className="flex items-center gap-2">
                                                <span className="text-xs text-zinc-500 font-bold uppercase tracking-wider w-16">Aktivní:</span>
                                                <select
                                                    value={activeSceneId}
                                                    onChange={(e) => switchScene(e.target.value)}
                                                    className="flex-1 h-8 px-3 bg-zinc-900/50 border border-zinc-700/50 rounded-lg text-zinc-200 text-sm appearance-none outline-none focus:border-blue-500/50 transition-all cursor-pointer"
                                                >
                                                    {displayScenes.map(s => (
                                                        <option key={s.id} value={s.id} className="bg-zinc-900">{s.name}</option>
                                                    ))}
                                                </select>
                                            </div>

                                            <div className="flex items-center gap-2">
                                                <span className="text-xs text-zinc-500 font-bold uppercase tracking-wider w-16">Název:</span>
                                                <input
                                                    value={displayScenes.find(s => s.id === activeSceneId)?.name || ''}
                                                    onChange={(e) => handleSceneNameChange(activeSceneId, e.target.value)}
                                                    disabled={activeSceneId === "0"}
                                                    className={`flex-1 h-8 px-3 bg-zinc-900/50 border border-zinc-700/50 rounded-lg text-zinc-200 text-sm focus:border-blue-500/50 outline-none transition-all placeholder-zinc-600 ${activeSceneId === "0" ? 'opacity-50 cursor-not-allowed' : ''}`}
                                                    placeholder="Přejmenovat scénu..."
                                                />
                                            </div>

                                            <div className="flex items-center justify-between pt-2 border-t border-zinc-700/30">
                                                <span className="text-xs text-zinc-500">{scenes.length} scén</span>
                                                <div className="flex items-center gap-2">
                                                    <button
                                                        onClick={exportScenes}
                                                        className="p-1.5 bg-zinc-700/30 hover:bg-zinc-700/50 rounded-lg text-zinc-400 hover:text-zinc-300 transition-all"
                                                        title="Exportovat scény"
                                                    >
                                                        <Download className="w-3 h-3" />
                                                    </button>

                                                    <label className="p-1.5 bg-zinc-700/30 hover:bg-zinc-700/50 rounded-lg text-zinc-400 hover:text-zinc-300 transition-all cursor-pointer" title="Importovat scény">
                                                        <Upload className="w-3 h-3" />
                                                        <input
                                                            type="file"
                                                            accept=".json"
                                                            onChange={importScenes}
                                                            className="hidden"
                                                        />
                                                    </label>

                                                    <button
                                                        onClick={generateSceneSummary}
                                                        className="p-1.5 bg-green-700/30 hover:bg-green-700/50 rounded-lg text-green-400 hover:text-green-300 transition-all"
                                                        title="Vytvořit soupis tahů"
                                                    >
                                                        <ClipboardList className="w-6 h-6" />
                                                    </button>

                                                    <button
                                                        onClick={() => {
                                                            if (confirm('Opravdu chcete smazat aktuální scénu?')) {
                                                                setScenes(prev => prev.filter(s => s.id !== activeSceneId));
                                                                if (scenes.length > 1) {
                                                                    setActiveSceneId(scenes.find(s => s.id !== activeSceneId)?.id || '');
                                                                }
                                                                addLog(`Scéna smazána`);
                                                            }
                                                        }}
                                                        className="text-xs text-red-400 hover:text-red-300 transition-colors"
                                                        disabled={scenes.length <= 1}
                                                    >
                                                        Smazat
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <LogPanel logs={logs} className={isPositioningMode ? 'opacity-20 blur-sm pointer-events-none transition-all duration-500' : 'transition-all duration-500'} />
                            </div>
                        </div>
                    </motion.aside>
                )}
            </AnimatePresence>

            <main className="flex-1 relative flex flex-col bg-[#050505] no-print">


                {/* Visual context info - Blurred when in positioning mode */}
                <div className="absolute top-0 right-0 left-0 h-32 pointer-events-none bg-gradient-to-b from-black/80 via-black/40 to-transparent z-30 px-12 pt-8 flex items-start justify-between no-print transition-all duration-500">
                    <div className="flex items-center gap-8">
                        {!isSidebarOpen && (
                            <button
                                onClick={() => setIsSidebarOpen(true)}
                                className={`p-4 bg-zinc-900/80 backdrop-blur-md border border-zinc-800 rounded-2xl shadow-2xl transition-all active:scale-95 text-blue-400 group z-40 pointer-events-auto ${isPositioningMode ? 'opacity-0 blur-md' : 'hover:bg-zinc-800'}`}
                            >
                                <Maximize2 className="w-6 h-6 group-hover:scale-110 transition-transform" />
                            </button>
                        )}
                        <div className={`flex flex-col gap-1 ml-[50px] transition-all duration-500 ${isPositioningMode ? 'opacity-0 blur-md' : ''}`}>
                            <h2 className="text-3xl font-black tracking-tighter text-white/90 uppercase">
                                {(() => {
                                    if (selectedTah) return `Scénický Tah ${selectedTahId}`;
                                    if (selectedVectorId) return 'Editace popisků - Vektorová Linka';
                                    if (selectedTextId) return 'Editace popisků - Textové pole';

                                    const toolNames: Record<string, string> = {
                                        select: 'Výběr',
                                        pen: 'Štětec',
                                        line: 'Rovná linka',
                                        text: 'Textové pole',
                                        eraser: 'Guma'
                                    };
                                    return `Editace popisků - ${toolNames[drawTool] || drawTool}`;
                                })()}
                            </h2>
                            <div className="flex items-center gap-3">
                                <span className={`w-2 h-2 rounded-full ${selectedTah?.isHanging ? 'bg-green-500 shadow-[0_0_8px_rgba(34,197,94,0.5)]' : 'bg-zinc-600'}`} />
                                <span className="text-xs font-bold text-zinc-500 uppercase tracking-widest">
                                    {selectedTah
                                        ? (selectedTah.isHanging ? `Zatížen: ${selectedTah.dek}cm + ${selectedTah.uva}cm` : 'Bez dekorace')
                                        : (selectedVectorId ? 'Úprava vektoru' : 'Vyberte tah nebo linku')
                                    }
                                </span>
                            </div>
                        </div>
                    </div>

                    <div className="flex items-start gap-4 z-40 pointer-events-auto" style={{ marginTop: '-7px' }}>
                        <button
                            onClick={() => window.print()}
                            className={`p-4 bg-zinc-900/80 backdrop-blur-md border border-zinc-800 rounded-2xl text-zinc-400 shadow-2xl transition-all duration-500 ${isPositioningMode ? 'opacity-0 blur-md pointer-events-none' : 'hover:text-white hover:bg-zinc-800 active:scale-95'}`}
                            title="Tisk všech scén"
                        >
                            <Printer className="w-6 h-6" />
                        </button>

                        <div className="flex flex-col gap-3 items-center">
                            <button
                                onClick={() => isPositioningMode ? handleTogglePositioningMode() : setIsSettingsOpen(true)}
                                className={`p-4 bg-zinc-900/80 backdrop-blur-md border border-zinc-800 rounded-2xl transition-all active:scale-95 shadow-2xl ${isPositioningMode
                                    ? 'text-amber-500 bg-amber-500/10 border-amber-500 shadow-[0_0_20px_rgba(245,158,11,0.3)]'
                                    : 'text-zinc-400 hover:text-white hover:bg-zinc-800'
                                    }`}
                            >
                                {isPositioningMode ? (
                                    <TriangleAlert className="w-6 h-6 animate-pulse" />
                                ) : (
                                    <Settings className="w-6 h-6" />
                                )}
                            </button>

                            {isPositioningMode && (
                                <motion.div
                                    initial={{ opacity: 0, y: -10 }}
                                    animate={{ opacity: 1, y: 0 }}
                                    className="bg-zinc-900/90 backdrop-blur-md border border-zinc-800 rounded-2xl p-3 shadow-2xl flex flex-col items-center gap-3"
                                >
                                    <div className="text-[9px] font-black text-amber-500 uppercase tracking-widest [writing-mode:vertical-lr] rotate-180 opacity-50">ZOOM</div>
                                    <input
                                        type="range"
                                        min="1"
                                        max="4"
                                        step="0.1"
                                        value={zoomLevel}
                                        onChange={(e) => setZoomLevel(parseFloat(e.target.value))}
                                        className="h-32 w-1.5 appearance-none bg-zinc-800 rounded-full cursor-pointer accent-amber-500 [&::-webkit-slider-thumb]:appearance-none [&::-webkit-slider-thumb]:w-3 [&::-webkit-slider-thumb]:h-3 [&::-webkit-slider-thumb]:rounded-full [&::-webkit-slider-thumb]:bg-amber-500"
                                        style={{ WebkitAppearance: 'slider-vertical' }}
                                    />
                                    <div className="text-[10px] font-mono font-bold text-zinc-400">{Math.round(zoomLevel * 100)}%</div>
                                </motion.div>
                            )}
                        </div>
                    </div>
                </div>

                <div className="flex-1 relative bg-black/40 flex items-start justify-start pt-[60px] px-6 pb-6 mt-[10px] overflow-auto custom-scrollbar">
                    <div className="w-full h-full relative flex flex-col items-center">
                        {/* Drawing Tools Overlay - Blurred when in positioning mode */}
                        <div className={`absolute bottom-8 left-1/2 -translate-x-1/2 glass-panel rounded-3xl p-2 flex items-center gap-2 z-40 border border-zinc-800 shadow-2xl bg-zinc-900/90 backdrop-blur-md no-print transition-all duration-500 ${isPositioningMode ? 'opacity-0 blur-md pointer-events-none' : ''}`}>
                            <button
                                onClick={() => setIsDrawingMode(!isDrawingMode)}
                                className={`p-4 rounded-2xl transition-all ${isDrawingMode ? 'bg-blue-600 text-white shadow-lg' : 'hover:bg-zinc-800 text-zinc-400'}`}
                                title="Kreslení (D)"
                            >
                                <Pencil className="w-6 h-6" />
                            </button>
                            <div className="w-[1px] h-8 bg-zinc-800 mx-2" />
                            <button
                                onClick={() => {
                                    setDrawTool('select');
                                    setIsDrawingMode(true);
                                    setSelectedVectorId(null);
                                    setSelectedTextId(null);
                                    setSelectedTahId(-1);
                                }}
                                className={`p-3 rounded-xl transition-all ${drawTool === 'select' && isDrawingMode ? 'bg-zinc-700 text-white' : 'hover:bg-zinc-800 text-zinc-400'}`}
                                title="Výběr / Přesun"
                            >
                                <MousePointer2 className="w-5 h-5" />
                            </button>
                            <input
                                type="color"
                                value={brushColor}
                                onChange={e => handleColorChange(e.target.value)}
                                className="w-10 h-10 rounded-xl border-2 border-zinc-700 cursor-pointer overflow-hidden p-0 bg-transparent hover:border-zinc-500 transition-colors"
                            />
                            <button
                                onClick={() => { setDrawTool('pen'); setIsDrawingMode(true); }}
                                className={`p-3 rounded-xl transition-all ${drawTool === 'pen' && isDrawingMode ? 'bg-zinc-700 text-white' : 'hover:bg-zinc-800 text-zinc-400'}`}
                                title="Štětec"
                            >
                                <Pencil className="w-5 h-5" />
                            </button>
                            <button
                                onClick={() => { setDrawTool('line'); setIsDrawingMode(true); }}
                                onDoubleClick={() => setEditingLineId('default')}
                                className={`p-3 rounded-xl transition-all ${drawTool === 'line' && isDrawingMode ? 'bg-zinc-700 text-white' : 'hover:bg-zinc-800 text-zinc-400'}`}
                                title="Rovná linka (Dvojklik pro nastavení)"
                            >
                                <motion.div style={{ rotate: 135 }}>
                                    <Minus className="w-5 h-5" />
                                </motion.div>
                            </button>
                            <button
                                onClick={() => { setDrawTool('text'); setIsDrawingMode(true); }}
                                className={`p-3 rounded-xl transition-all ${drawTool === 'text' && isDrawingMode ? 'bg-zinc-700 text-white' : 'hover:bg-zinc-800 text-zinc-400'}`}
                                title="Textové pole (T)"
                            >
                                <Type className="w-5 h-5" />
                            </button>
                            <button
                                onClick={generateSceneSummary}
                                className="p-3 bg-green-900/40 hover:bg-green-700/60 rounded-xl text-green-400 hover:text-green-300 transition-all active:scale-95"
                                title="Vytvořit soupis tahů (S)"
                            >
                                <ClipboardList className="w-6 h-6" />
                            </button>
                            <button
                                onClick={() => { setDrawTool('eraser'); setIsDrawingMode(true); }}
                                className={`p-3 rounded-xl transition-all ${drawTool === 'eraser' && isDrawingMode ? 'bg-zinc-700 text-white' : 'hover:bg-zinc-800 text-zinc-400'}`}
                                title="Guma"
                            >
                                <Eraser className="w-5 h-5" />
                            </button>
                        </div>

                        {/* Main Stage Canvas: Not Blurred, but pass positioning props */}
                        <motion.div
                            className="relative glass-panel rounded-3xl shadow-[0_64px_128px_-32px_rgba(0,0,0,0.7)] p-0 bg-[#0a0a0a] border border-zinc-800/50"
                            style={{
                                transformOrigin: `${selectedTahId !== -1 ? (hoistPositions[selectedTahId]?.x ?? 500) : (hoistPositions[1]?.x ?? 500)}px ${stageConfig.topLimitY}px`
                            }}
                            animate={{ scale: zoomLevel }}
                            transition={{ type: 'spring', damping: 25, stiffness: 200 }}
                        >
                            <StageCanvas
                                tahy={tahy}
                                selectedId={selectedTahId}
                                onSelectTah={setSelectedTahId}
                                onUpdateTah={updateTah}
                                drawingColor={brushColor}
                                drawTool={drawTool}
                                isDrawingEnabled={isDrawingMode}
                                stageConfig={stageConfig}
                                highlightY={focusedPixelValue}
                                vectorLines={vectorLines}
                                selectedVectorId={selectedVectorId}
                                showVectorHandles={drawTool === 'select' || drawTool === 'text'}
                                onUpdateVectorLines={handleUpdateVectorLines}
                                onSelectVector={(id) => {
                                    setSelectedVectorId(id);
                                    if (id) {
                                        setSelectedTahId(-1);
                                        setSelectedTextId(null);
                                    }
                                }}
                                onLineDoubleClick={(id) => setEditingLineId(id)}
                                defaultLineStyle={defaultLineStyle}
                                defaultLineWidth={defaultLineWidth}
                                textLabels={textLabels}
                                selectedTextId={selectedTextId}
                                onUpdateTextLabels={handleUpdateTextLabels}
                                onSelectText={(id) => {
                                    setSelectedTextId(id);
                                    if (id) {
                                        setSelectedTahId(-1);
                                        setSelectedVectorId(null);
                                    }
                                }}
                                onTextDoubleClick={(id) => setEditingTextId(id)}
                                hoistPositions={hoistPositions}
                                onUpdateHoistPositions={setHoistPositions}
                                isPositioningMode={isPositioningMode}
                                onUpdateTopLimitY={(y) => updateStageConfig({ topLimitY: y })}
                            />
                        </motion.div>
                    </div>
                </div>

                {/* Settings Modal */}
                <AnimatePresence>
                    {isSettingsOpen && (
                        <motion.div
                            initial={{ opacity: 0 }}
                            animate={{ opacity: 1 }}
                            exit={{ opacity: 0 }}
                            className="absolute inset-0 z-50 flex items-center justify-center p-8 bg-black/60"
                            onClick={() => setIsSettingsOpen(false)}
                        >
                            <motion.div
                                initial={{ scale: 0.95, opacity: 0, y: 20 }}
                                animate={{ scale: 1, opacity: 1, y: 0 }}
                                exit={{ scale: 0.95, opacity: 0, y: 20 }}
                                className="w-full max-w-xl bg-zinc-900 border border-zinc-800 rounded-[3rem] shadow-[0_32px_64px_-16px_rgba(0,0,0,0.8)] p-12 space-y-10"
                                onClick={(e: React.MouseEvent) => e.stopPropagation()}
                            >
                                <div className="flex items-center justify-between">
                                    <div className="flex items-center gap-4">
                                        <div className="p-4 bg-blue-500/10 rounded-2xl">
                                            <Settings className="w-8 h-8 text-blue-400" />
                                        </div>
                                        <div>
                                            <h2 className="text-2xl font-black text-white tracking-tight uppercase leading-none mb-1">Globální nastavení</h2>
                                            <p className="text-xs text-zinc-500 font-bold uppercase tracking-widest">Konfigurace geometrie scény</p>
                                        </div>
                                    </div>
                                    <button
                                        onClick={() => setIsSettingsOpen(false)}
                                        className="p-3 hover:bg-zinc-800 rounded-2xl text-zinc-500 transition-all hover:text-white"
                                    >
                                        <X className="w-8 h-8" />
                                    </button>
                                </div>

                                <div className="grid grid-cols-2 gap-8">
                                    <div className="space-y-3">
                                        <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest pl-1">Vzdálenost Podlaha - tah v nejvyšší poloze (cm)</label>
                                        <input
                                            type="number"
                                            value={stageConfig.stageHeightCm}
                                            onChange={e => updateStageConfig({ stageHeightCm: Number(e.target.value) })}
                                            className="w-full bg-zinc-950 border-2 border-zinc-800/50 rounded-2xl px-6 py-5 text-zinc-100 font-mono text-xl focus:border-blue-500 outline-none transition-all shadow-inner"
                                        />
                                    </div>
                                    <div className="space-y-3">
                                        <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest pl-1">Červená ryska (limit nad podlahou cm)</label>
                                        <input
                                            type="number"
                                            value={stageConfig.minHeightCm}
                                            onChange={e => updateStageConfig({ minHeightCm: Number(e.target.value) })}
                                            className="w-full bg-zinc-950 border-2 border-zinc-800/50 rounded-2xl px-6 py-5 text-zinc-100 font-mono text-xl focus:border-red-500 outline-none transition-all shadow-inner"
                                        />
                                    </div>
                                    <div className="space-y-3">
                                        <label className="text-[10px] font-black text-blue-400 uppercase tracking-widest pl-1">Pozice Kladek (px)</label>
                                        <input
                                            type="number"
                                            value={stageConfig.topLimitY}
                                            onChange={e => updateStageConfig({ topLimitY: Number(e.target.value) })}
                                            onFocus={() => setFocusedPixelValue(stageConfig.topLimitY)}
                                            onBlur={() => setFocusedPixelValue(null)}
                                            className="w-full bg-zinc-950 border-2 border-blue-900/30 rounded-2xl px-6 py-5 text-blue-400 font-mono text-xl focus:border-blue-500 outline-none transition-all shadow-inner"
                                        />
                                    </div>
                                    <div className="space-y-3">
                                        <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest pl-1">Pozice Podlahy (px)</label>
                                        <input
                                            type="number"
                                            value={stageConfig.zeroLevelY}
                                            onChange={e => updateStageConfig({ zeroLevelY: Number(e.target.value) })}
                                            onFocus={() => setFocusedPixelValue(stageConfig.zeroLevelY)}
                                            onBlur={() => setFocusedPixelValue(null)}
                                            className="w-full bg-zinc-950 border-2 border-zinc-800/50 rounded-2xl px-6 py-5 text-zinc-100 font-mono text-xl focus:border-blue-500 outline-none transition-all shadow-inner"
                                        />
                                    </div>
                                    <div className="space-y-3">
                                        <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest pl-1">Šířka kulisy (px)</label>
                                        <input
                                            type="number"
                                            value={stageConfig.decorationWidth}
                                            onChange={e => updateStageConfig({ decorationWidth: Number(e.target.value) })}
                                            className="w-full bg-zinc-950 border-2 border-zinc-800/50 rounded-2xl px-6 py-5 text-zinc-100 font-mono text-xl focus:border-blue-500 outline-none transition-all shadow-inner"
                                        />
                                    </div>
                                    <div className="col-span-2 space-y-4">
                                        <div className="flex justify-between items-end pl-1">
                                            <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest">Podkladový obrázek</label>
                                            <span className="text-[9px] font-bold text-blue-400 uppercase tracking-widest bg-blue-500/10 px-2 py-1 rounded-md">Doporučené rozlišení: 1125 × 789 px</span>
                                        </div>
                                        <div className="flex gap-4">
                                            <label className="flex-1 flex items-center justify-center gap-2 bg-zinc-800 hover:bg-zinc-700 text-white font-bold py-4 rounded-2xl cursor-pointer transition-all active:scale-[0.98] text-xs uppercase tracking-widest">
                                                <Upload className="w-4 h-4" />
                                                Nahrát nový obrázek
                                                <input
                                                    type="file"
                                                    accept="image/*"
                                                    onChange={handleImageUpload}
                                                    className="hidden"
                                                />
                                            </label>
                                            {stageConfig.customBgImage && (
                                                <button
                                                    onClick={resetBackgroundImage}
                                                    className="px-6 bg-red-500/10 border border-red-500/20 text-red-400 font-bold rounded-2xl hover:bg-red-500/20 transition-all active:scale-[0.98] text-xs uppercase tracking-widest"
                                                >
                                                    Reset
                                                </button>
                                            )}
                                        </div>
                                    </div>
                                    <div className="col-span-2 p-6 bg-zinc-950/50 border border-zinc-800 rounded-3xl flex items-center justify-between">
                                        <div className="flex items-center gap-3">
                                            <Info className="w-5 h-5 text-blue-500" />
                                            <span className="text-[10px] font-black text-zinc-500 uppercase tracking-widest">Přímo vypočítané měřítko</span>
                                        </div>
                                        <div className="text-xl font-mono text-blue-400 font-bold tracking-tighter">
                                            {stageConfig.scale.toFixed(4)} <span className="text-xs text-zinc-600 ml-1">px/cm</span>
                                        </div>
                                    </div>
                                    {/* Manual Positioning Toggle */}
                                    <div className="col-span-2 grid grid-cols-2 gap-4">
                                        <button
                                            onClick={handleTogglePositioningMode}
                                            className={`py-4 rounded-2xl font-black text-xs flex items-center justify-center gap-3 transition-all active:scale-[0.98] uppercase tracking-[0.1em] w-full ${isPositioningMode
                                                ? 'bg-amber-500 text-black shadow-[0_0_30px_rgba(245,158,11,0.4)] animate-pulse'
                                                : 'bg-zinc-800 text-amber-500 hover:bg-zinc-700 hover:text-amber-400'
                                                }`}
                                        >
                                            <TriangleAlert className="w-5 h-5" />
                                            {isPositioningMode ? 'Stop Parametry' : 'Pozicování a parametry tahů'}
                                        </button>
                                        <button
                                            onClick={() => {
                                                setIsHoistConfigMode(!isHoistConfigMode);
                                                if (!isHoistConfigMode) {
                                                    setIsSidebarOpen(false); // Hide sidebar to see the footer list better
                                                }
                                            }}
                                            className={`py-4 rounded-2xl font-black text-xs flex items-center justify-center gap-3 transition-all active:scale-[0.98] uppercase tracking-[0.1em] w-full ${isHoistConfigMode
                                                ? 'bg-blue-500 text-white shadow-[0_0_30px_rgba(59,130,246,0.4)] animate-pulse'
                                                : 'bg-zinc-800 text-blue-400 hover:bg-zinc-700 hover:text-blue-300'
                                                }`}
                                        >
                                            <Settings className="w-5 h-5" />
                                            {isHoistConfigMode ? 'Stop Nosnost' : 'Nosnost'}
                                        </button>
                                    </div>
                                </div>

                                <button
                                    onClick={() => setIsSettingsOpen(false)}
                                    className="w-full bg-white text-black font-black py-6 rounded-2xl hover:bg-zinc-200 transition-all active:scale-[0.98] shadow-xl uppercase tracking-[0.2em] text-xs"
                                >
                                    Uložit konfiguraci
                                </button>
                            </motion.div>
                        </motion.div>
                    )}
                </AnimatePresence>
            </main >

            {/* Line Settings Modal */}
            <AnimatePresence>
                {
                    editingLineId && (
                        <motion.div
                            initial={{ opacity: 0 }}
                            animate={{ opacity: 1 }}
                            exit={{ opacity: 0 }}
                            className="fixed inset-0 z-[100] flex items-center justify-center p-8 bg-black/60 backdrop-blur-sm"
                            onClick={() => setEditingLineId(null)}
                        >
                            {(() => {
                                const isDefault = editingLineId === 'default';
                                const line = isDefault ? {
                                    id: 'default',
                                    color: brushColor,
                                    lineStyle: defaultLineStyle,
                                    lineWidth: defaultLineWidth
                                } : vectorLines.find(l => l.id === editingLineId);

                                if (!line) return null;
                                return (
                                    <motion.div
                                        initial={{ scale: 0.9, opacity: 0, y: 20 }}
                                        animate={{ scale: 1, opacity: 1, y: 0 }}
                                        exit={{ scale: 0.9, opacity: 0, y: 20 }}
                                        className="w-full max-w-md bg-zinc-900 border border-zinc-800 rounded-[2.5rem] shadow-2xl p-10 space-y-8"
                                        onClick={(e) => e.stopPropagation()}
                                    >
                                        <div className="flex items-center justify-between">
                                            <div className="flex items-center gap-4">
                                                <div className="p-3 bg-blue-500/10 rounded-xl">
                                                    <Settings className="w-6 h-6 text-blue-400" />
                                                </div>
                                                <h3 className="text-xl font-black text-white uppercase tracking-tight">
                                                    {isDefault ? 'Výchozí nastavení linky' : 'Nastavení linky'}
                                                </h3>
                                            </div>
                                            <button
                                                onClick={() => setEditingLineId(null)}
                                                className="p-2 hover:bg-zinc-800 rounded-xl text-zinc-500 transition-colors"
                                            >
                                                <X className="w-6 h-6" />
                                            </button>
                                        </div>

                                        <div className="space-y-6">
                                            {/* Style Selector */}
                                            <div className="space-y-3">
                                                <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest pl-1">Styl čáry</label>
                                                <div className="grid grid-cols-3 gap-2">
                                                    {(['solid', 'dashed', 'dotted'] as const).map(style => (
                                                        <button
                                                            key={style}
                                                            onClick={() => updateVectorLine(line.id, { lineStyle: style })}
                                                            className={`py-3 rounded-xl border-2 font-bold text-xs transition-all ${line.lineStyle === style ? 'bg-blue-600/20 border-blue-500 text-blue-400' : 'bg-zinc-950 border-zinc-800 text-zinc-500 hover:border-zinc-700'}`}
                                                        >
                                                            {style === 'solid' ? 'Plná' : style === 'dashed' ? 'Čárkovaná' : 'Tečkovaná'}
                                                        </button>
                                                    ))}
                                                </div>
                                            </div>

                                            {/* Thickness Selector */}
                                            <div className="space-y-3">
                                                <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest pl-1">Tloušťka ({line.lineWidth || 2}px)</label>
                                                <div className="flex items-center gap-4 bg-zinc-950 border border-zinc-800 rounded-xl px-4 py-2">
                                                    <input
                                                        type="range"
                                                        min="1"
                                                        max="20"
                                                        value={line.lineWidth || 2}
                                                        onChange={(e) => updateVectorLine(line.id, { lineWidth: Number(e.target.value) })}
                                                        className="flex-1 accent-blue-500 h-1.5 rounded-lg appearance-none bg-zinc-800 cursor-pointer"
                                                    />
                                                </div>
                                            </div>

                                            {/* Color Selector */}
                                            <div className="space-y-3">
                                                <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest pl-1">Barva</label>
                                                <div className="flex items-center gap-4 bg-zinc-950 border border-zinc-800 rounded-xl px-4 py-3">
                                                    <input
                                                        type="color"
                                                        value={line.color}
                                                        onChange={(e) => updateVectorLine(line.id, { color: e.target.value })}
                                                        className="w-10 h-10 rounded-lg border-2 border-zinc-800 cursor-pointer bg-transparent overflow-hidden p-0"
                                                    />
                                                    <span className="text-sm font-mono text-zinc-400 uppercase tracking-tighter">{line.color}</span>
                                                </div>
                                            </div>

                                            {/* Preview */}
                                            <div className="pt-4 mt-4 border-t border-zinc-800">
                                                <div className="w-full h-12 bg-black rounded-xl border border-zinc-800 flex items-center justify-center">
                                                    <div
                                                        style={{
                                                            width: '80%',
                                                            height: line.lineWidth || 2,
                                                            backgroundColor: line.color,
                                                            borderRadius: line.lineStyle === 'dotted' ? '100px' : '0'
                                                        }}
                                                        className={line.lineStyle === 'dashed' ? 'bg-transparent' : ''}
                                                    >
                                                        {line.lineStyle === 'dashed' && (
                                                            <div className="w-full h-full" style={{
                                                                backgroundImage: `linear-gradient(to right, ${line.color} 50%, transparent 50%)`,
                                                                backgroundSize: `${(line.lineWidth || 2) * 5}px 100%`
                                                            }} />
                                                        )}
                                                        {line.lineStyle === 'dotted' && (
                                                            <div className="w-full h-full" style={{
                                                                backgroundImage: `radial-gradient(circle, ${line.color} 30%, transparent 35%)`,
                                                                backgroundSize: `${(line.lineWidth || 2) * 3}px 100%`
                                                            }} />
                                                        )}
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div className="grid grid-cols-2 gap-4">
                                            {!isDefault && (
                                                <button
                                                    onClick={() => {
                                                        setVectorLines(prev => {
                                                            const next = prev.filter(l => l.id !== line.id);
                                                            syncCurrentStateToScenes(undefined, next, undefined);
                                                            return next;
                                                        });
                                                        setEditingLineId(null);
                                                        setSelectedVectorId(null);
                                                    }}
                                                    className="bg-red-500/10 border border-red-500/20 text-red-400 font-bold py-4 rounded-xl hover:bg-red-500/20 transition-all active:scale-[0.98] uppercase tracking-widest text-[10px]"
                                                >
                                                    Smazat
                                                </button>
                                            )}
                                            <button
                                                onClick={() => {
                                                    setEditingLineId(null);
                                                    setSelectedVectorId(null);
                                                }}
                                                className={`${isDefault ? 'col-span-2' : ''} bg-white text-black font-black py-4 rounded-xl hover:bg-zinc-200 transition-all active:scale-[0.98] uppercase tracking-[0.2em] text-[10px]`}
                                            >
                                                Hotovo
                                            </button>
                                        </div>
                                    </motion.div>
                                );
                            })()}
                        </motion.div>
                    )
                }

                {
                    editingTextId && (
                        <motion.div
                            initial={{ opacity: 0 }}
                            animate={{ opacity: 1 }}
                            exit={{ opacity: 0 }}
                            className="fixed inset-0 z-[100] flex items-center justify-center p-8 bg-black/60 backdrop-blur-sm"
                            onClick={() => setEditingTextId(null)}
                        >
                            {(() => {
                                const label = textLabels.find(l => l.id === editingTextId);
                                if (!label) return null;
                                return (
                                    <motion.div
                                        initial={{ scale: 0.9, opacity: 0, y: 20 }}
                                        animate={{ scale: 1, opacity: 1, y: 0 }}
                                        exit={{ scale: 0.9, opacity: 0, y: 20 }}
                                        className="w-full max-w-md bg-zinc-900 border border-zinc-800 rounded-[2.5rem] shadow-2xl p-10 space-y-8"
                                        onClick={(e) => e.stopPropagation()}
                                    >
                                        <div className="flex items-center justify-between">
                                            <div className="flex items-center gap-4">
                                                <div className="p-3 bg-purple-500/10 rounded-xl">
                                                    <Type className="w-6 h-6 text-purple-400" />
                                                </div>
                                                <h3 className="text-xl font-black text-white uppercase tracking-tight">Nastavení textu</h3>
                                            </div>
                                            <button
                                                onClick={() => setEditingTextId(null)}
                                                className="p-2 hover:bg-zinc-800 rounded-xl text-zinc-500 transition-colors"
                                            >
                                                <X className="w-6 h-6" />
                                            </button>
                                        </div>

                                        <div className="space-y-6">
                                            {/* Text Content */}
                                            <div className="space-y-3">
                                                <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest pl-1">Text</label>
                                                <textarea
                                                    autoFocus
                                                    value={label.text}
                                                    onChange={(e) => updateTextLabel(label.id, { text: e.target.value })}
                                                    onFocus={(e) => e.target.setSelectionRange(e.target.value.length, e.target.value.length)}
                                                    rows={4}
                                                    className="w-full bg-zinc-950 border border-zinc-800 rounded-xl px-4 py-3 text-white focus:ring-2 focus:ring-purple-500/30 outline-none resize-none text-sm leading-relaxed"
                                                    placeholder="Napište svůj text..."
                                                />
                                            </div>

                                            {/* Font Size */}
                                            <div className="space-y-3">
                                                <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest pl-1">Velikost ({label.fontSize}px)</label>
                                                <div className="flex items-center gap-4 bg-zinc-950 border border-zinc-800 rounded-xl px-4 py-2">
                                                    <input
                                                        type="range"
                                                        min="8"
                                                        max="72"
                                                        value={label.fontSize}
                                                        onChange={(e) => updateTextLabel(label.id, { fontSize: Number(e.target.value) })}
                                                        className="flex-1 accent-purple-500 h-1.5 rounded-lg appearance-none bg-zinc-800 cursor-pointer"
                                                    />
                                                </div>
                                            </div>

                                            {/* Color Selection Group */}
                                            <div className="grid grid-cols-2 gap-4">
                                                {/* Text Color */}
                                                <div className="space-y-3">
                                                    <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest pl-1">Barva textu</label>
                                                    <div className="flex items-center gap-4 bg-zinc-950 border border-zinc-800 rounded-xl px-4 py-3">
                                                        <input
                                                            type="color"
                                                            value={label.color}
                                                            onChange={(e) => updateTextLabel(label.id, { color: e.target.value })}
                                                            className="w-8 h-8 rounded-lg border-2 border-zinc-800 cursor-pointer bg-transparent overflow-hidden p-0"
                                                        />
                                                        <span className="text-[10px] font-mono text-zinc-400 font-bold uppercase">{label.color}</span>
                                                    </div>
                                                </div>

                                                {/* Background Color & Opacity */}
                                                <div className="space-y-4">
                                                    <div className="flex justify-between items-center pl-1">
                                                        <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest">Pozadí</label>
                                                        <input
                                                            type="checkbox"
                                                            checked={!!label.backgroundColor}
                                                            onChange={(e) => updateTextLabel(label.id, {
                                                                backgroundColor: e.target.checked ? '#ffffff' : undefined,
                                                                backgroundOpacity: e.target.checked ? 0.4 : undefined
                                                            })}
                                                            className="w-4 h-4 accent-purple-500 rounded border-zinc-700 bg-zinc-800"
                                                        />
                                                    </div>

                                                    {label.backgroundColor && (
                                                        <div className="space-y-3 p-3 bg-zinc-950 border border-zinc-800 rounded-xl">
                                                            <div className="flex items-center gap-3">
                                                                <input
                                                                    type="color"
                                                                    value={label.backgroundColor.substring(0, 7)}
                                                                    onChange={(e) => updateTextLabel(label.id, { backgroundColor: e.target.value })}
                                                                    className="w-10 h-10 rounded-lg border-2 border-zinc-800 cursor-pointer bg-transparent overflow-hidden p-0"
                                                                />
                                                                <div className="flex-1 space-y-1">
                                                                    <div className="flex justify-between text-[10px] font-black text-zinc-500 uppercase tracking-widest px-1">
                                                                        <span>Průhlednost</span>
                                                                        <span>{Math.round((label.backgroundOpacity ?? 0.3) * 100)}%</span>
                                                                    </div>
                                                                    <input
                                                                        type="range"
                                                                        min="0"
                                                                        max="1"
                                                                        step="0.05"
                                                                        value={label.backgroundOpacity ?? 0.3}
                                                                        onChange={(e) => updateTextLabel(label.id, { backgroundOpacity: parseFloat(e.target.value) })}
                                                                        className="w-full h-1.5 bg-zinc-800 rounded-lg appearance-none cursor-pointer accent-purple-500"
                                                                    />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    )}
                                                </div>
                                            </div>
                                        </div>

                                        <div className="grid grid-cols-2 gap-4 pt-4">
                                            <button
                                                onClick={() => removeTextLabel(label.id)}
                                                className="bg-red-500/10 border border-red-500/20 text-red-400 font-bold py-4 rounded-xl hover:bg-red-500/20 transition-all active:scale-[0.98] uppercase tracking-widest text-[10px]"
                                            >
                                                Smazat
                                            </button>
                                            <button
                                                onClick={() => {
                                                    setEditingTextId(null);
                                                    setSelectedTextId(null);
                                                }}
                                                className="bg-white text-black font-black py-4 rounded-xl hover:bg-zinc-200 transition-all active:scale-[0.98] uppercase tracking-widest text-[10px]"
                                            >
                                                Hotovo
                                            </button>
                                        </div>
                                    </motion.div>
                                );
                            })()}
                        </motion.div>
                    )
                }
            </AnimatePresence >

            {!isSidebarOpen && (
                <div className="min-h-20 h-auto glass-panel border-t border-zinc-800/50 bg-zinc-900/50 backdrop-blur-xl flex flex-col p-8 gap-6 no-print w-full">
                    <div className="flex items-center justify-between w-full border-b border-zinc-800 pb-4">
                        <div className="flex flex-col gap-1">
                            <div className="text-xl font-black text-zinc-100 uppercase tracking-tighter">Soupis a stav všech tahů</div>
                            <div className="text-[10px] font-black text-zinc-500 uppercase tracking-[0.2em]">Technické parametry a aktuální zatížení</div>
                        </div>
                        <div className="flex flex-col items-end leading-none">
                            <span className="text-[10px] font-black text-zinc-500 uppercase tracking-widest mb-1">Měřítko scény</span>
                            <span className="text-zinc-200 font-mono font-bold">1 cm = {stageConfig.scale.toFixed(3)} px</span>
                        </div>
                    </div>

                    <div className="w-full overflow-hidden">
                        <table className="w-full text-left border-collapse">
                            <thead>
                                <tr className="text-[10px] font-black text-zinc-500 uppercase tracking-widest border-b border-zinc-800">
                                    <th className="pb-3 pl-2">Tah č.</th>
                                    <th className="pb-3 text-amber-500">Nosnost</th>
                                    <th className="pb-3">Celková výška</th>
                                    <th className="pb-3">Dekorace</th>
                                    <th className="pb-3 text-red-500">Úvazek</th>
                                    <th className="pb-3 text-blue-400">Název kulisy</th>
                                    <th className="pb-3">Stav</th>
                                </tr>
                            </thead>
                            <tbody className="divide-y divide-zinc-800/50">
                                {TAH_IDS.map(id => {
                                    const t = tahy[id] || { id, pod: 0, dek: 0, uva: 0, nosnost: 80 };
                                    const totalHeight = t.pod + t.dek + t.uva;
                                    const isSelected = selectedTahId === id;
                                    const isLocked = t.funkce === 'LOCK';
                                    const isLight = t.funkce === 'světla';

                                    const rowClass = `group transition-colors hover:bg-zinc-800/30 cursor-pointer ${isSelected ? 'bg-blue-500/10' : ''} ${isLocked ? 'text-red-500/50' : ''}`;
                                    const cellClass = isLocked ? 'line-through decoration-red-500/50' : '';

                                    return (
                                        <tr key={id} onClick={() => setSelectedTahId(id)} className={rowClass}>
                                            <td className={`py-2 pl-2 font-black ${isLocked ? 'text-red-500' : 'text-zinc-400'}`}>
                                                #{id}{isLocked && <span className="ml-1 text-[8px] uppercase font-black">LOCK</span>}
                                            </td>
                                            <td className={`py-2 ${cellClass}`}>
                                                {isHoistConfigMode ? (
                                                    <input
                                                        type="number"
                                                        value={t.nosnost ?? 80}
                                                        onChange={(e) => updateTah(id, { nosnost: Number(e.target.value) })}
                                                        onClick={(e) => e.stopPropagation()}
                                                        className="w-16 bg-zinc-950 border border-amber-500/30 rounded px-1 text-amber-500 font-mono text-xs outline-none focus:border-amber-500"
                                                    />
                                                ) : (
                                                    <span className={`font-mono font-bold ${isLocked ? 'text-red-500/50' : 'text-amber-500'}`}>{t.nosnost ?? 80} kg</span>
                                                )}
                                            </td>
                                            <td className={`py-2 font-mono ${isLocked ? 'text-red-500/50' : 'text-zinc-100'} ${cellClass}`}>{totalHeight} cm</td>
                                            <td className={`py-2 font-mono ${isLocked ? 'text-red-500/50' : 'text-zinc-400'} ${cellClass}`}>{t.isHanging ? `${t.dek} cm` : '-'}</td>
                                            <td className={`py-2 font-mono ${isLocked ? 'text-red-500/30' : 'text-red-500/80'} font-bold ${cellClass}`}>{t.isHanging && t.uva > 0 ? `${t.uva} cm` : '-'}</td>
                                            <td className="py-2">
                                                <span className={`font-bold text-xs truncate max-w-[200px] block ${isLocked ? 'text-red-500/50 italic' : (isLight ? 'text-zinc-500' : (t.isHanging ? 'text-blue-400' : 'text-zinc-700 italic'))}`}>
                                                    {isLocked ? 'UZAMČENO' : (isLight ? 'SVĚTELNÝ TAH' : (t.isHanging ? (t.name || '(Bez názvu)') : 'Prázdný'))}
                                                </span>
                                            </td>
                                            <td className="py-2">
                                                <div className={`w-2.5 h-2.5 rounded-full ${isLocked ? 'bg-red-500 animate-pulse shadow-[0_0_10px_rgba(239,68,68,0.5)]' :
                                                    isLight ? 'bg-yellow-500 shadow-[0_0_10px_rgba(234,179,8,0.5)]' :
                                                        t.isHanging ? 'bg-green-500 shadow-[0_0_8px_rgba(34,197,94,0.5)]' : 'bg-zinc-800'
                                                    }`} />
                                            </td>
                                        </tr>
                                    );
                                })}
                            </tbody>
                        </table>
                    </div>
                </div>
            )}

            {/* Print Container */}
            <div className="print-only">
                {scenes.map((scene) => (
                    <div key={scene.id} className="print-page">
                        <div className="w-full flex justify-center">
                            <StageCanvas
                                tahy={scene.tahy}
                                stageConfig={stageConfig}
                                vectorLines={scene.vectorLines || []}
                                textLabels={scene.textLabels || []}
                                selectedId={-1}
                                selectedVectorId={null}
                                selectedTextId={null}
                                onSelectTah={() => { }}
                                onUpdateTah={() => { }}
                                drawingColor="#000000"
                                drawTool="select"
                                isDrawingEnabled={false}
                                onUpdateVectorLines={() => { }}
                                onSelectVector={() => { }}
                                onUpdateTextLabels={() => { }}
                                onSelectText={() => { }}
                                showVectorHandles={false}
                                hoistPositions={hoistPositions}
                                cropLeft={PRINT_VIEWPORT_X}
                                cropTop={PRINT_VIEWPORT_Y}
                                cropRight={1125 - PRINT_VIEWPORT_X - PRINT_VIEWPORT_W}
                                cropBottom={789 - PRINT_VIEWPORT_Y - PRINT_VIEWPORT_H}

                            />
                        </div>
                    </div>
                ))}
            </div>
        </div >
    );
};

export default App;
