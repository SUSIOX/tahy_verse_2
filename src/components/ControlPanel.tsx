import * as React from 'react';
import { useState, useEffect } from 'react';
import { TahState, TAH_IDS, StageConfig, HoistRegistry } from '../types';
import { Anchor, ArrowUp, ArrowDownUp, XCircle, Trash2, Info, ChevronUp, ChevronDown, TriangleAlert, Settings, RotateCcw } from 'lucide-react';

interface SmartBtnProps {
    onStep: (dir: 1 | -1) => void;
    direction: 1 | -1;
    className?: string;
}

const SmartStepButton: React.FC<SmartBtnProps> = ({ onStep, direction, className }) => {
    const timerRef = React.useRef<any>(null);
    const startRef = React.useRef<number>(0);

    const startStepping = () => {
        startRef.current = Date.now();
        onStep(direction); // First click immediately

        const step = () => {
            const elapsed = Date.now() - startRef.current;
            // DMX-like acceleration logic
            let count = 1;
            if (elapsed > 6000) count = 100;
            else if (elapsed > 3000) count = 10;

            for (let i = 0; i < count; i++) {
                onStep(direction);
            }
            timerRef.current = setTimeout(step, 100);
        };

        // Initial delay before repeating
        timerRef.current = setTimeout(step, 400);
    };

    const stopStepping = () => {
        if (timerRef.current) {
            clearTimeout(timerRef.current);
            timerRef.current = null;
        }
    };

    return (
        <button
            onMouseDown={(e) => { e.preventDefault(); startStepping(); }}
            onMouseUp={stopStepping}
            onMouseLeave={stopStepping}
            onTouchStart={(e) => { e.preventDefault(); startStepping(); }}
            onTouchEnd={stopStepping}
            className={`${className} select-none cursor-pointer active:bg-zinc-700 transition-colors`}
        >
            {direction === 1 ? <ChevronUp className="w-5 h-5" /> : <ChevronDown className="w-5 h-5" />}
        </button>
    );
};

interface Props {
    selectedId: number;
    onSelectId: (id: number) => void;
    tah: TahState;
    onUpdate: (id: number, updatesOrFn: Partial<TahState> | ((prev: TahState) => Partial<TahState>)) => void;
    onHang: (id: number, dek: number, uva: number, pod: number) => void;
    onClearAll: () => void;
    onResetToDefault: () => void;
    onAddLog: (msg: string) => void;
    stageConfig: StageConfig;
    onUpdateConfig: (config: StageConfig) => void;
    onTogglePositioningMode: () => void;
    onUpdateHoistPositions: (positions: HoistRegistry) => void;
    isPositioningMode: boolean;
    tahy: Record<number, TahState>;
    hoistPositions: HoistRegistry;
}

const ControlPanel: React.FC<Props> = ({
    selectedId,
    onSelectId,
    tah,
    onUpdate,
    onClearAll,
    onResetToDefault,
    onAddLog,
    stageConfig,
    onUpdateConfig,
    onHang,
    onTogglePositioningMode,
    onUpdateHoistPositions,
    isPositioningMode,
    tahy,
    hoistPositions
}) => {
    if (!tah) {
        return (
            <div className="flex flex-col items-center justify-center p-8 bg-zinc-900/30 border border-dashed border-zinc-700 rounded-3xl text-zinc-500 gap-4">
                <Info className="w-12 h-12 opacity-20" />
                <div className="text-center space-y-1">
                    <p className="font-bold text-sm uppercase tracking-wider">Není vybrán tah</p>
                    <p className="text-[10px]">Klikněte na tah pro jeho úpravu</p>
                </div>
            </div>
        );
    }

    const isLocked = tah.funkce === 'LOCK';
    const isLight = tah.funkce === 'světla';

    const handleAction = (field: 'dek' | 'uva' | 'pod', dir: 1 | -1) => {
        if (isPositioningMode) return;
        if (isLocked) return;
        onUpdate(selectedId, (current) => {
            const currentVal = Number(current[field]) || 0;
            let newVal = currentVal + dir;

            // Boundaries
            if (newVal < 0) newVal = 0;
            if (field === 'pod' && newVal > stageConfig.stageHeightCm) newVal = stageConfig.stageHeightCm;

            const updates: Partial<TahState> = { [field]: newVal };
            if (field === 'dek' || field === 'uva') {
                const updatedDek = field === 'dek' ? newVal : current.dek;
                const updatedUva = field === 'uva' ? newVal : current.uva;
                updates.isHanging = updatedDek > 0 || updatedUva > 0;
            }
            return updates;
        });
    };

    const handleDekChange = (val: string) => {
        if (isPositioningMode || isLocked) return;
        const num = Math.max(0, Number(val) || 0);
        onUpdate(selectedId, { dek: num, isHanging: num > 0 || tah.uva > 0 });
    };

    const handleUvaChange = (val: string) => {
        if (isPositioningMode || isLocked) return;
        const num = Math.max(0, Number(val) || 0);
        onUpdate(selectedId, { uva: num, isHanging: num > 0 || tah.dek > 0 });
    };

    const handlePodChange = (val: string) => {
        if (isLocked) return;
        if (isPositioningMode && !isLight) return;
        const num = Math.min(stageConfig.stageHeightCm, Math.max(0, Number(val) || 0));
        onUpdate(selectedId, { pod: num });
    };

    const handleHangClick = () => {
        if (isPositioningMode || isLight || isLocked) return;
        onHang(selectedId, 0, 0, 0); // Parameters are ignored in App.tsx, defaults are applied there
    };

    const handleUnhang = () => {
        if (isPositioningMode || isLocked) return;
        onUpdate(selectedId, { isHanging: false, dek: 0, uva: 0, pod: 0, name: '' });
        onAddLog(`Tah ${selectedId}: Dekorace odvěšena`);
    };

    const handleTopLimit = () => {
        if (isPositioningMode || isLocked) return;
        const newState = !tah.isTopLimit;
        let updates: Partial<TahState> = { isTopLimit: newState };
        if (newState) {
            const { stageHeightCm } = stageConfig;
            // Snaps hook to stageHeightCm
            const maxPod = stageHeightCm - (tah.dek + tah.uva);
            updates.pod = maxPod;
            onAddLog(`Tah ${selectedId}: Horní úvrať ZAPNUTA (vytahování na ${stageHeightCm}cm)`);
        } else {
            onAddLog(`Tah ${selectedId}: Horní úvrať VYPNUTA`);
        }
        onUpdate(selectedId, updates);
    };

    const handleBottomLimit = () => {
        if (isPositioningMode || isLocked) return;
        const newState = !tah.isBottomLimit;
        let updates: Partial<TahState> = { isBottomLimit: newState };
        if (newState) {
            const { minHeightCm } = stageConfig;
            // Snaps HOOK to the Red Line (minHeightCm)
            // pod = hookHeight - (dek + uva)
            const limitPod = minHeightCm - (tah.dek + tah.uva);
            updates.pod = limitPod;
            onAddLog(`Tah ${selectedId}: Dolní úvrať ZAPNUTA (hák na limit: ${minHeightCm}cm)`);
        } else {
            onAddLog(`Tah ${selectedId}: Dolní úvrať VYPNUTA`);
        }
        onUpdate(selectedId, updates);
    };


    return (
        <div className="space-y-8">

            <div className="space-y-4">
                <div className="flex items-center justify-between">
                    <label className="text-[10px] font-bold text-zinc-500 uppercase tracking-[0.2em]">{isPositioningMode ? 'Parametry tahu' : 'Konfigurace tahu'}</label>
                    <div className="flex gap-2">
                        <button
                            onClick={onResetToDefault}
                            disabled={isPositioningMode}
                            className="text-zinc-500 hover:text-zinc-300 text-[10px] flex items-center gap-1.5 transition-all hover:bg-white/5 px-2 py-1 rounded disabled:opacity-20"
                            title="Nastaví všechny tahy na výchozí hodnoty (0)"
                        >
                            <RotateCcw className="w-3 h-3" /> DEFAULT
                        </button>
                        {/* <button
                            onClick={onClearAll}
                            disabled={isPositioningMode}
                            className="text-red-500/80 hover:text-red-400 text-[10px] flex items-center gap-1.5 transition-all hover:bg-red-500/5 px-2 py-1 rounded disabled:opacity-20"
                        >
                            <Trash2 className="w-3 h-3" /> VYMAZAT VŠE
                        </button> */}
                    </div>
                </div>


                <div className="relative">
                    <select
                        value={selectedId}
                        onChange={(e) => onSelectId(Number(e.target.value))}
                        className={`w-full bg-zinc-800/80 border rounded-xl px-4 py-4 text-xl font-bold appearance-none transition-all outline-none ${isLocked ? 'border-red-500/50 text-red-500 shadow-[0_0_15px_rgba(239,68,68,0.1)]' :
                            isLight ? 'border-yellow-500/30 text-yellow-500' :
                                'border-zinc-700/50 text-blue-400 focus:ring-2 focus:ring-blue-500/30'
                            }`}
                    >
                        {TAH_IDS.map(id => {
                            const t = tahy[id];
                            const iL = t?.funkce === 'světla';
                            const iK = t?.funkce === 'LOCK';
                            return (
                                <option
                                    key={id}
                                    value={id}
                                    className={`${iL ? 'text-yellow-600' : iK ? 'text-red-500 font-bold' : 'text-blue-400'}`}
                                >
                                    Tah č. {id} {iK ? ' (LOCK)' : iL ? ' (SVĚTLA)' : ''}
                                </option>
                            );
                        })}
                    </select>
                    <div className="absolute right-4 top-1/2 -translate-y-1/2 pointer-events-none flex flex-col items-end gap-1">
                        <div className="text-[9px] font-bold text-zinc-500 uppercase tracking-widest">Aktivní tah</div>
                        <div className={`w-2.5 h-2.5 rounded-full ${isLocked ? 'bg-red-500 animate-pulse shadow-[0_0_8px_rgba(239,68,68,0.5)]' :
                            isLight ? 'bg-yellow-500 shadow-[0_0_8px_rgba(234,179,8,0.5)]' :
                                tah.isHanging ? 'bg-green-500 animate-pulse' : 'bg-zinc-600'
                            }`} />
                    </div>
                </div>
            </div>

            {/* New Fields for Positioning Mode */}
            {isPositioningMode && (
                <div className="space-y-4 animate-in fade-in slide-in-from-top-4 duration-500">
                    <div className="grid grid-cols-2 gap-4">
                        <div className="space-y-2">
                            <label className="text-[10px] font-black text-amber-500 uppercase flex items-center gap-2">
                                <TriangleAlert className="w-3 h-3" /> Nosnost (kg)
                            </label>
                            <input
                                type="number"
                                value={tah.nosnost || 80}
                                onChange={(e) => onUpdate(selectedId, { nosnost: Number(e.target.value) })}
                                className="w-full bg-zinc-950 border border-amber-500/30 rounded-xl px-4 py-3 text-lg font-mono text-amber-500 outline-none focus:border-amber-500 transition-all shadow-inner"
                            />
                        </div>
                        <div className="space-y-2">
                            <label className="text-[10px] font-black text-blue-400 uppercase flex items-center gap-2">
                                <Settings className="w-3 h-3" /> Funkce
                            </label>
                            <div className="relative">
                                <select
                                    value={tah.funkce || 'kulisy'}
                                    onChange={(e) => onUpdate(selectedId, { funkce: e.target.value as any })}
                                    className={`w-full bg-zinc-950 border rounded-xl px-4 py-3 text-sm font-bold outline-none focus:border-blue-500 transition-all appearance-none cursor-pointer ${isLocked ? 'border-red-500 text-red-500' :
                                        isLight ? 'border-yellow-500 text-yellow-500' :
                                            'border-blue-500/30 text-blue-400'
                                        }`}
                                >
                                    <option value="kulisy">KULISY</option>
                                    <option value="světla" className="text-yellow-500">SVĚTLA</option>
                                    <option value="LOCK" className="text-red-500 font-bold">LOCK</option>
                                </select>
                                <div className="absolute right-3 top-1/2 -translate-y-1/2 pointer-events-none text-blue-400/50">
                                    <ChevronDown className="w-4 h-4" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="p-5 bg-amber-500/10 border border-amber-500/20 rounded-2xl space-y-4">
                        <div className="flex items-center justify-between">
                            <label className="text-[10px] font-black text-amber-500 uppercase tracking-widest">Horizontální Pozice X (px)</label>
                            <span className="text-[10px] font-mono text-amber-500/50">Tah č. {selectedId}</span>
                        </div>
                        <div className="flex gap-2">
                            <input
                                type="number"
                                value={Math.round(hoistPositions[selectedId]?.x ?? 0)}
                                onChange={(e) => onUpdateHoistPositions({
                                    ...hoistPositions,
                                    [selectedId]: { x: Number(e.target.value) }
                                })}
                                className="flex-1 bg-zinc-950 border border-amber-500/30 rounded-xl px-4 py-3 text-lg font-mono text-amber-400 outline-none focus:border-amber-500"
                            />
                            <button
                                onClick={() => {
                                    // Reset to fallback
                                    import('../types').then(types => {
                                        onUpdateHoistPositions({
                                            ...hoistPositions,
                                            [selectedId]: { x: types.DEFAULT_HOIST_POSITIONS[selectedId].x }
                                        });
                                        onAddLog(`Tah ${selectedId}: Pozice X resetována na výchozí`);
                                    });
                                }}
                                className="px-4 bg-zinc-800 hover:bg-zinc-700 text-zinc-400 hover:text-white rounded-xl transition-all text-[10px] font-bold uppercase tracking-widest"
                            >
                                Reset
                            </button>
                        </div>
                        {(hoistPositions[selectedId]?.x ?? 0) > 1125 && (
                            <div className="text-[9px] text-red-400 font-bold animate-pulse">
                                ⚠️ TAH JE MIMO PLÁTNO (X &gt; 1125px)
                            </div>
                        )}
                    </div>
                </div>
            )}

            <div className={`grid grid-cols-2 gap-4 transition-all duration-500 ${isPositioningMode ? 'opacity-20 grayscale pointer-events-none scale-95 origin-top' : ''}`}>
                <div className="space-y-2">
                    <label className="text-[10px] font-bold text-zinc-500 uppercase flex items-center gap-2">
                        <div className="w-1.5 h-1.5 rounded-full bg-blue-500" /> Výška dekorace
                    </label>
                    <div className="flex bg-zinc-900 border border-zinc-800 rounded-xl overflow-hidden focus-within:border-blue-500/50 transition-all">
                        <SmartStepButton onStep={(d) => handleAction('dek', d)} direction={-1} className="px-3 hover:bg-zinc-800 text-zinc-500 border-r border-zinc-800" />
                        <input
                            type="number"
                            value={tah.dek}
                            onChange={e => handleDekChange(e.target.value)}
                            className="w-full bg-transparent px-2 py-3 text-lg font-mono text-blue-300 outline-none text-center [appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none"
                            placeholder="0"
                        />
                        <SmartStepButton onStep={(d) => handleAction('dek', d)} direction={1} className="px-3 hover:bg-zinc-800 text-zinc-500 border-l border-zinc-800" />
                    </div>
                </div>
                <div className="space-y-2">
                    <label className="text-[10px] font-bold text-zinc-500 uppercase flex items-center gap-2">
                        <div className="w-1.5 h-1.5 rounded-full bg-red-500" /> Délka úvazku
                    </label>
                    <div className="flex bg-zinc-900 border border-zinc-800 rounded-xl overflow-hidden focus-within:border-red-500/50 transition-all">
                        <SmartStepButton onStep={(d) => handleAction('uva', d)} direction={-1} className="px-3 hover:bg-zinc-800 text-zinc-500 border-r border-zinc-800" />
                        <input
                            type="number"
                            value={tah.uva}
                            onChange={e => handleUvaChange(e.target.value)}
                            className="w-full bg-transparent px-2 py-3 text-lg font-mono text-red-300 outline-none text-center [appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none"
                            placeholder="0"
                        />
                        <SmartStepButton onStep={(d) => handleAction('uva', d)} direction={1} className="px-3 hover:bg-zinc-800 text-zinc-500 border-l border-zinc-800" />
                    </div>
                </div>
            </div>

            <div className={`bg-zinc-900/40 p-5 rounded-2xl border border-zinc-800/50 space-y-4 transition-all duration-500 ${isPositioningMode && !isLight ? 'opacity-20 grayscale pointer-events-none scale-95 origin-top' : ''}`}>
                <div className="space-y-2">
                    <label className={`${isLight ? 'text-yellow-500' : 'text-zinc-500'} text-[10px] font-bold uppercase flex items-center gap-2 transition-colors`}>
                        {isLight ? 'VÝŠKA SVĚTELNÉHO TAHU' : `Zvednuto od podlahy (0-${stageConfig.stageHeightCm} cm)`}
                    </label>
                    <div className={`flex bg-zinc-950 border rounded-xl overflow-hidden transition-all ${isLight ? 'border-yellow-500/50 focus-within:border-yellow-500' : 'border-zinc-800 focus-within:border-zinc-500'}`}>
                        <SmartStepButton onStep={(d) => handleAction('pod', d)} direction={-1} className="px-4 hover:bg-zinc-900 text-zinc-500 border-r border-zinc-800" />
                        <input
                            type="number"
                            value={tah.pod}
                            onChange={e => handlePodChange(e.target.value)}
                            className={`w-full bg-transparent px-2 py-4 font-mono outline-none text-center text-xl [appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none ${isLight ? 'text-yellow-400' : 'text-zinc-100'}`}
                            placeholder="0"
                        />
                        <SmartStepButton onStep={(d) => handleAction('pod', d)} direction={1} className="px-4 hover:bg-zinc-900 text-zinc-500 border-l border-zinc-800" />
                    </div>
                </div>
            </div>

            <div className={`flex flex-col gap-3 pt-2 transition-all duration-500 ${isPositioningMode ? 'opacity-20 grayscale pointer-events-none scale-95 origin-top' : ''}`}>
                <button
                    onClick={handleHangClick}
                    disabled={isLight || isLocked}
                    className={`group relative w-full py-5 rounded-2xl font-bold flex items-center justify-center gap-3 transition-all active:scale-[0.98] overflow-hidden disabled:bg-zinc-800 disabled:text-zinc-600 disabled:shadow-none shadow-[0_8px_30px_-10px_rgba(37,99,235,0.4)] ${isLocked ? 'bg-red-900/20 border border-red-500/50 text-red-500' :
                        isLight ? 'bg-yellow-900/20 border border-yellow-500/50 text-yellow-500' :
                            'bg-blue-600 hover:bg-blue-500 text-white'
                        }`}
                >
                    <div className="absolute inset-0 bg-gradient-to-r from-transparent via-white/10 to-transparent -translate-x-full group-hover:animate-shimmer" />
                    <Anchor className="w-6 h-6" /> {isLight ? 'SVĚTELNÝ TAH (NELZE VĚŠET)' : (isLocked ? 'TAH UZAMČEN' : 'ZAVĚSIT DEKORACI')}
                </button>

                {/* Decoration Name Input - Shows only when hanging */}
                {tah.isHanging && (
                    <div className="space-y-2 animate-in fade-in slide-in-from-top-2 duration-300">
                        <label className="text-[10px] font-bold text-blue-400 uppercase flex items-center gap-2">
                            <div className="w-1.5 h-1.5 rounded-full bg-blue-400" /> Název dekorace
                        </label>
                        <input
                            type="text"
                            value={tah.name || ''}
                            onChange={(e) => onUpdate(selectedId, { name: e.target.value })}
                            className="w-full bg-zinc-900 border border-blue-500/30 rounded-xl px-4 py-3 text-sm font-bold text-zinc-100 outline-none focus:border-blue-500 transition-all placeholder-zinc-700"
                            placeholder="Např. Opona, Les, Praktikábly..."
                        />
                    </div>
                )}

                <div className="grid grid-cols-2 gap-3">
                    <button
                        onClick={handleTopLimit}
                        disabled={isLocked}
                        className={`py-4 px-2 rounded-xl border-2 flex items-center justify-center gap-2 font-bold text-[10px] transition-all active:scale-95 disabled:opacity-30 ${tah.isTopLimit
                            ? 'bg-amber-500/20 border-amber-500 text-amber-500 shadow-[0_0_15px_-5px_rgba(245,158,11,0.5)]'
                            : 'bg-zinc-900 border-zinc-800 text-zinc-500 hover:bg-zinc-800 hover:text-zinc-400'
                            }`}
                    >
                        <ArrowUp className="w-4 h-4" /> HORNÍ ÚVRAŤ
                    </button>

                    <button
                        onClick={handleBottomLimit}
                        disabled={isLocked}
                        className={`py-4 px-2 rounded-xl border-2 flex items-center justify-center gap-2 font-bold text-[10px] transition-all active:scale-95 disabled:opacity-30 ${tah.isBottomLimit
                            ? 'bg-blue-500/20 border-blue-500 text-blue-500 shadow-[0_0_15px_-5px_rgba(59,130,246,0.5)]'
                            : 'bg-zinc-900 border-zinc-800 text-zinc-500 hover:bg-zinc-800 hover:text-zinc-400'
                            }`}
                    >
                        <ArrowDownUp className="w-4 h-4 rotate-180" /> DOLNÍ ÚVRAŤ
                    </button>
                </div>

                <button
                    onClick={handleUnhang}
                    disabled={!tah.isHanging || isLocked}
                    className="w-full bg-zinc-900 border-2 border-zinc-800 text-zinc-600 font-bold text-xs py-4 px-4 rounded-xl flex items-center justify-center gap-2 enabled:hover:bg-red-950/20 enabled:hover:border-red-900/50 enabled:hover:text-red-400 transition-all active:scale-95 disabled:opacity-30 disabled:cursor-not-allowed"
                >
                    <XCircle className="w-4 h-4" /> ODVĚSIT
                </button>

            </div>

            <div className="flex items-start gap-3 p-4 bg-blue-900/10 rounded-xl border border-blue-900/20">
                <Info className="w-4 h-4 text-blue-500 mt-0.5 flex-shrink-0" />
                <p className="text-[10px] text-zinc-500 leading-relaxed italic">
                    Součet výšky dekorace a úvazku nesmí překročit 914 cm. Výška od podlahy ovlivňuje pozici celého tahu.
                </p>
            </div>
        </div>
    );
};

export default ControlPanel;
