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
    TextLabel
} from './types';
import StageCanvas from './components/StageCanvas';
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
    Printer
} from 'lucide-react';

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
                isBottomLimit: false
            };
        });
        return initial;
    };

    // Initial state with some test data
    const [tahy, setTahy] = useState<Record<number, TahState>>(getInitialTahy);

    // Scene Management State
    const [productionName, setProductionName] = useState(() => {
        const saved = localStorage.getItem('tahy-production-name');
        return saved || "Moje Inscenace";
    });
    const [activeSceneId, setActiveSceneId] = useState(() => {
        const saved = localStorage.getItem('tahy-active-scene-id');
        return saved || "1";
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
        return [{ id: "1", name: "Scéna 1", tahy: getInitialTahy() }];
    });

    const [selectedTahId, setSelectedTahId] = useState<number>(TAH_IDS[0]);
    const [logs, setLogs] = useState<string[]>([]);
    const [isSidebarOpen, setIsSidebarOpen] = useState(true);
    const [stageConfig, setStageConfig] = useState<StageConfig>(DEFAULT_STAGE_CONFIG);
    const [isSettingsOpen, setIsSettingsOpen] = useState(false);
    const [focusedPixelValue, setFocusedPixelValue] = useState<number | null>(null);

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

    // Initialize tahy from the active scene when component mounts or scenes change
    useEffect(() => {
        const activeScene = scenes.find(s => s.id === activeSceneId);
        if (activeScene) {
            setTahy(activeScene.tahy);
        }
    }, [activeSceneId, scenes]);

    const updateStageConfig = useCallback((updates: Partial<StageConfig>) => {
        setStageConfig((prev: StageConfig) => {
            const next = { ...prev, ...updates };
            // Scale = Delta Px / Delta Cm
            const distancePx = next.zeroLevelY - next.topLimitY;
            next.scale = distancePx / next.stageHeightCm;
            return next;
        });
    }, []);

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

    const updateTextLabel = (id: string, updates: Partial<TextLabel>) => {
        setTextLabels(prev => prev.map(l => l.id === id ? { ...l, ...updates } : l));
    };

    const removeTextLabel = (id: string) => {
        setTextLabels(prev => prev.filter(l => l.id !== id));
        if (selectedTextId === id) setSelectedTextId(null);
        if (editingTextId === id) setEditingTextId(null);
    };

    const updateVectorLine = (id: string, updates: Partial<VectorLine>) => {
        if (id === 'default') {
            if (updates.lineStyle) setDefaultLineStyle(updates.lineStyle);
            if (updates.lineWidth) setDefaultLineWidth(updates.lineWidth);
            if (updates.color) setBrushColor(updates.color);
            return;
        }
        setVectorLines(prev => prev.map(l => l.id === id ? { ...l, ...updates } : l));
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
        const scene = scenes.find(s => s.id === sceneId);
        if (scene) {
            setActiveSceneId(sceneId);
            setTahy(scene.tahy);
            addLog(`Přepnuto na scénu: ${scene.name}`);
        }
    };

    const handleSceneNameChange = (id: string, newName: string) => {
        setScenes(prev => prev.map(s => s.id === id ? { ...s, name: newName } : s));
    };

    const exportScenes = () => {
        const data = {
            productionName,
            scenes,
            activeSceneId,
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
                        addLog(`Scény importovány: ${data.scenes.length} scén`);
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


    const updateTah = useCallback((id: number, updatesOrFn: Partial<TahState> | ((prev: TahState) => Partial<TahState>)) => {
        setTahy(prev => {
            const currentTah = prev[id];
            const updates = typeof updatesOrFn === 'function' ? updatesOrFn(currentTah) : updatesOrFn;
            const newTahy = {
                ...prev,
                [id]: { ...currentTah, ...updates }
            };

            // Sync with current scene
            setScenes(prevScenes => prevScenes.map(s =>
                s.id === activeSceneId ? { ...s, tahy: newTahy } : s
            ));

            return newTahy;
        });
    }, [activeSceneId]);

    const selectedTah = tahy[selectedTahId];

    return (
        <div className="flex h-screen bg-[#050505] text-zinc-100 font-sans selection:bg-blue-500/30 overflow-hidden">
            <AnimatePresence mode="wait">
                {isSidebarOpen && (
                    <motion.aside
                        initial={{ width: 0, opacity: 0 }}
                        animate={{ width: 400, opacity: 1 }}
                        exit={{ width: 0, opacity: 0 }}
                        className="h-full flex flex-col z-40 overflow-hidden relative border-r border-zinc-800/50 bg-zinc-900/50 backdrop-blur-xl shadow-2xl flex-shrink-0"
                    >
                        <div className="p-8 border-b border-zinc-800/50 bg-zinc-900/50">
                            <div className="flex items-center justify-between mb-8">
                                <div className="flex items-center gap-3">
                                    <div className="w-10 h-10 bg-blue-600 rounded-2xl flex items-center justify-center shadow-lg shadow-blue-600/20 rotate-3 group-hover:rotate-0 transition-transform">
                                        <div className="w-5 h-5 border-2 border-white rounded-sm" />
                                    </div>
                                    <div>
                                        <h1 className="text-xl font-black tracking-tighter uppercase leading-none">Tahy Jirka</h1>
                                        <span className="text-[10px] text-zinc-500 font-bold tracking-[0.2em] uppercase">Verse 2.0</span>
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

                        <div className="flex-1 overflow-y-auto custom-scrollbar bg-zinc-900/20">
                            <div className="p-8 space-y-8">
                                <ControlPanel
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
                                    onAddLog={addLog}
                                    stageConfig={stageConfig}
                                    onUpdateConfig={setStageConfig}
                                />

                                {/* Production and Scene Management Panel */}
                                <div className="bg-zinc-800/30 border border-zinc-700/50 rounded-2xl p-6 space-y-6">
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
                                                    {scenes.map(s => (
                                                        <option key={s.id} value={s.id} className="bg-zinc-900">{s.name}</option>
                                                    ))}
                                                </select>
                                            </div>

                                            <div className="flex items-center gap-2">
                                                <span className="text-xs text-zinc-500 font-bold uppercase tracking-wider w-16">Název:</span>
                                                <input
                                                    value={scenes.find(s => s.id === activeSceneId)?.name || ''}
                                                    onChange={(e) => handleSceneNameChange(activeSceneId, e.target.value)}
                                                    className="flex-1 h-8 px-3 bg-zinc-900/50 border border-zinc-700/50 rounded-lg text-zinc-200 text-sm focus:border-blue-500/50 outline-none transition-all placeholder-zinc-600"
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

                                <LogPanel logs={logs} />
                            </div>
                        </div>
                    </motion.aside>
                )}
            </AnimatePresence>

            <main className="flex-1 relative flex flex-col bg-[#050505]">


                {/* Visual context info */}
                <div className="absolute top-0 right-0 left-0 h-32 pointer-events-none bg-gradient-to-b from-black/80 via-black/40 to-transparent z-30 px-12 pt-8 flex items-start justify-between no-print">
                    <div className="flex items-center gap-8">
                        {!isSidebarOpen && (
                            <button
                                onClick={() => setIsSidebarOpen(true)}
                                className="p-4 bg-zinc-900/80 backdrop-blur-md border border-zinc-800 rounded-2xl shadow-2xl hover:bg-zinc-800 transition-all active:scale-95 text-blue-400 group z-40 pointer-events-auto"
                            >
                                <Maximize2 className="w-6 h-6 group-hover:scale-110 transition-transform" />
                            </button>
                        )}
                        <div className="flex flex-col gap-1 ml-[50px]">
                            <h2 className="text-3xl font-black tracking-tighter text-white/90 uppercase">
                                {selectedTah ? `Scénický Tah ${selectedTahId}` : (selectedVectorId ? 'Vektorová Linka' : 'Žádný výběr')}
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

                    <div className="flex items-center gap-4 z-40 pointer-events-auto">
                        <button
                            onClick={() => window.print()}
                            className="p-4 bg-zinc-900/80 backdrop-blur-md border border-zinc-800 rounded-2xl text-zinc-400 hover:text-white hover:bg-zinc-800 transition-all active:scale-95 shadow-2xl"
                            title="Tisk všech scén"
                        >
                            <Printer className="w-6 h-6" />
                        </button>
                        <button
                            onClick={() => setIsSettingsOpen(true)}
                            className="p-4 bg-zinc-900/80 backdrop-blur-md border border-zinc-800 rounded-2xl text-zinc-400 hover:text-white hover:bg-zinc-800 transition-all active:scale-95 shadow-2xl"
                        >
                            <Settings className="w-6 h-6" />
                        </button>
                    </div>
                </div>

                <div className="flex-1 relative bg-black/40 flex items-center justify-center p-12 mt-[10px]">
                    <div className="w-full h-full relative flex flex-col items-center">
                        {/* Drawing Tools Overlay */}
                        <div className="absolute bottom-8 left-1/2 -translate-x-1/2 glass-panel rounded-3xl p-2 flex items-center gap-2 z-40 border border-zinc-800 shadow-2xl bg-zinc-900/90 backdrop-blur-md no-print">
                            <button
                                onClick={() => setIsDrawingMode(!isDrawingMode)}
                                className={`p-4 rounded-2xl transition-all ${isDrawingMode ? 'bg-blue-600 text-white shadow-lg' : 'hover:bg-zinc-800 text-zinc-400'}`}
                                title="Kreslení (D)"
                            >
                                <Pencil className="w-6 h-6" />
                            </button>
                            <div className="w-[1px] h-8 bg-zinc-800 mx-2" />
                            <button
                                onClick={() => { setDrawTool('select'); setIsDrawingMode(true); }}
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
                                title="Textové pole"
                            >
                                <Type className="w-5 h-5" />
                            </button>
                            <button
                                onClick={() => { setDrawTool('eraser'); setIsDrawingMode(true); }}
                                className={`p-3 rounded-xl transition-all ${drawTool === 'eraser' && isDrawingMode ? 'bg-zinc-700 text-white' : 'hover:bg-zinc-800 text-zinc-400'}`}
                                title="Guma"
                            >
                                <Eraser className="w-5 h-5" />
                            </button>
                        </div>

                        <div className="relative glass-panel rounded-[3rem] overflow-hidden shadow-[0_64px_128px_-32px_rgba(0,0,0,0.7)] p-8 bg-[#0a0a0a] border border-zinc-800/50">
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
                                onUpdateVectorLines={setVectorLines}
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
                                onUpdateTextLabels={setTextLabels}
                                onSelectText={(id) => {
                                    setSelectedTextId(id);
                                    if (id) {
                                        setSelectedTahId(-1);
                                        setSelectedVectorId(null);
                                    }
                                }}
                                onTextDoubleClick={(id) => setEditingTextId(id)}
                            />
                        </div>
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
                                    <div className="space-y-3">
                                        <label className="text-[10px] font-black text-zinc-500 uppercase tracking-widest pl-1">Šířka kulisy (px)</label>
                                        <input
                                            type="number"
                                            value={stageConfig.decorationWidth}
                                            onChange={e => updateStageConfig({ decorationWidth: Number(e.target.value) })}
                                            className="w-full bg-zinc-950 border-2 border-zinc-800/50 rounded-2xl px-6 py-5 text-zinc-100 font-mono text-xl focus:border-blue-500 outline-none transition-all shadow-inner"
                                        />
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
            </main>

            {/* Line Settings Modal */}
            <AnimatePresence>
                {editingLineId && (
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

                                    <button
                                        onClick={() => setEditingLineId(null)}
                                        className="w-full bg-white text-black font-black py-4 rounded-xl hover:bg-zinc-200 transition-all active:scale-[0.98] uppercase tracking-[0.2em] text-[10px]"
                                    >
                                        Hotovo
                                    </button>
                                </motion.div>
                            );
                        })()}
                    </motion.div>
                )}

                {editingTextId && (
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
                                            onClick={() => setEditingTextId(null)}
                                            className="bg-white text-black font-black py-4 rounded-xl hover:bg-zinc-200 transition-all active:scale-[0.98] uppercase tracking-widest text-[10px]"
                                        >
                                            Hotovo
                                        </button>
                                    </div>
                                </motion.div>
                            );
                        })()}
                    </motion.div>
                )}
            </AnimatePresence>

            <div className="h-20 glass-panel border-t border-zinc-800/50 bg-zinc-900/50 backdrop-blur-xl flex items-center px-12 gap-16 text-sm no-print">
                <div className="flex-1" />
                <div className="flex items-center gap-6">
                    <div className="flex flex-col items-end leading-none">
                        <span className="text-[10px] font-black text-zinc-500 uppercase tracking-widest mb-1">Měřítko scény</span>
                        <span className="text-zinc-200 font-mono font-bold">1 cm = {stageConfig.scale.toFixed(3)} px</span>
                    </div>
                </div>
            </div>

            {/* Print Container */}
            <div className="print-only">
                {scenes.map((scene) => (
                    <div key={scene.id} className="print-page">
                        <div className="p-8 w-full">
                            <div className="flex justify-between items-end border-b-2 border-black pb-4 mb-6">
                                <div>
                                    <h1 className="text-3xl font-black uppercase tracking-tighter">{productionName}</h1>
                                    <h2 className="text-xl font-bold text-zinc-600">Scéna: {scene.name}</h2>
                                </div>
                                <div className="text-right text-sm font-mono text-zinc-400">
                                    Vytvořeno v aplikaci Tahy & Kulisář
                                </div>
                            </div>

                            <div className="flex justify-center bg-white border border-zinc-200">
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
                                />
                            </div>

                            <div className="mt-8 grid grid-cols-2 gap-8">
                                <div className="space-y-4">
                                    <h3 className="font-black uppercase tracking-wider text-xs border-b border-zinc-200 pb-2">Poznámky k tahům</h3>
                                    <div className="text-sm space-y-2">
                                        {Object.values(scene.tahy)
                                            .filter(t => t.isHanging || t.dek > 0)
                                            .sort((a, b) => a.id - b.id)
                                            .map(t => (
                                                <div key={t.id} className="flex gap-4">
                                                    <span className="font-bold w-12">Tah {t.id}:</span>
                                                    <span>{t.dek}cm dekorace, {t.uva}cm úvazek, {t.pod}cm od země</span>
                                                </div>
                                            ))
                                        }
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default App;
