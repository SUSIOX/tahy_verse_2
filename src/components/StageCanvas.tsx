import * as React from 'react';
import { useMemo } from 'react';
import { TahState, TAH_IDS, StageConfig, Point, VectorLine, LineStyle, TextLabel } from '../types';
import { motion, AnimatePresence } from 'framer-motion';
import DrawingLayer from './DrawingLayer';
import stageBgImage from '/assets/stage-bg.jpg?url';

interface Props {
    tahy: Record<number, TahState>;
    selectedId: number;
    onSelectTah: (id: number) => void;
    onUpdateTah: (id: number, updates: Partial<TahState>) => void;
    drawingColor: string;
    drawTool: 'pen' | 'eraser' | 'line' | 'select' | 'text';
    isDrawingEnabled: boolean;
    stageConfig: StageConfig;
    highlightY?: number | null;
    vectorLines: VectorLine[];
    selectedVectorId: string | null;
    showVectorHandles: boolean;
    onUpdateVectorLines: (lines: VectorLine[]) => void;
    onSelectVector: (id: string | null) => void;
    onLineDoubleClick?: (id: string) => void;
    defaultLineStyle?: LineStyle;
    defaultLineWidth?: number;
    textLabels: TextLabel[];
    selectedTextId: string | null;
    onUpdateTextLabels: (labels: TextLabel[]) => void;
    onSelectText: (id: string | null) => void;
    onTextDoubleClick?: (id: string) => void;
}

// Natural image dimensions from the file
const IMG_WIDTH = 1264;
const IMG_HEIGHT = 649;

/**
 * PIXEL-PERFECT COORDINATES
 * Extracted directly from the blueprint image centers.
 * Horizontal row at Y = 38.16
 */
const HOIST_HOOK_CENTERS: Record<number, { x: number }> = {
    1: { x: 834.79 },
    2: { x: 848.79 },
    3: { x: 862.79 },
    5: { x: 899.79 },
    6: { x: 913.79 },
    7: { x: 928.79 },
    8: { x: 942.79 },
    9: { x: 956.79 },
    10: { x: 970.79 },
    12: { x: 1003.79 },
    13: { x: 1017.79 },
    14: { x: 1031.79 },
    15: { x: 1069.79 },
    17: { x: 1096.79 },
    18: { x: 1110.79 }
};

// Wire starts exactly from the hook center
// HOIST_TOP_Y is now coming from stageConfig.topLimitY

// Stage component handles rendering of all hoists

const StageCanvas: React.FC<Props> = ({
    tahy,
    selectedId,
    onSelectTah,
    onUpdateTah,
    drawingColor,
    drawTool,
    isDrawingEnabled,
    stageConfig,
    highlightY,
    vectorLines,
    selectedVectorId,
    showVectorHandles,
    onUpdateVectorLines,
    onSelectVector,
    onLineDoubleClick,
    defaultLineStyle = 'dashed',
    defaultLineWidth = 1,
    textLabels,
    selectedTextId,
    onUpdateTextLabels,
    onSelectText,
    onTextDoubleClick
}: Props) => {
    const {
        stageHeightCm = 900,
        minHeightCm = 45,
        zeroLevelY = 482,
        topLimitY = 43,
        scale: scaleConfig = 0.4878,
        decorationWidth = 18
    } = stageConfig || {};
    const HOIST_TOP_Y = topLimitY;
    const ZERO_Y = zeroLevelY;
    const SCALE_FACTOR = scaleConfig;
    const DECORATION_HALF_WIDTH = decorationWidth / 2;
    const [draggingId, setDraggingId] = React.useState<number | null>(null);
    const [slingAdjustId, setSlingAdjustId] = React.useState<number | null>(null);
    const [dekAdjustId, setDekAdjustId] = React.useState<number | null>(null);
    const [resizeMode, setResizeMode] = React.useState<'top' | 'bottom' | null>(null);
    const [dragOffset, setDragOffset] = React.useState<number>(0); // Y offset when drag starts

    // Vector line drawing state
    const [pendingLine, setPendingLine] = React.useState<Point | null>(null);
    const [dragLineInfo, setDragLineInfo] = React.useState<{ id: string, point: 'start' | 'end' } | null>(null);
    const [mousePos, setMousePos] = React.useState<Point>({ x: 0, y: 0 });
    const [draggingTextId, setDraggingTextId] = React.useState<string | null>(null);
    const [textDragOffset, setTextDragOffset] = React.useState<Point>({ x: 0, y: 0 });

    // Clear pending line if tool changes or drawing is disabled
    React.useEffect(() => {
        if (!isDrawingEnabled || drawTool !== 'line') {
            setPendingLine(null);
        }
    }, [isDrawingEnabled, drawTool]);

    const handleMouseMove = (e: React.MouseEvent) => {
        const svg = e.currentTarget as SVGSVGElement;
        const ctm = svg.getScreenCTM();
        if (!ctm) return;

        const pt = svg.createSVGPoint();
        pt.x = e.clientX;
        pt.y = e.clientY;
        const svgP = pt.matrixTransform(ctm.inverse());
        const currentPos = { x: svgP.x, y: svgP.y };
        setMousePos(currentPos);

        // Text label dragging
        if (draggingTextId && showVectorHandles) {
            onUpdateTextLabels(textLabels.map(label => {
                if (label.id === draggingTextId) {
                    return { ...label, pos: { x: currentPos.x - textDragOffset.x, y: currentPos.y - textDragOffset.y } };
                }
                return label;
            }));
            return;
        }

        // Vector line endpoint dragging
        if (dragLineInfo && showVectorHandles) {
            onUpdateVectorLines(vectorLines.map(l => {
                if (l.id === dragLineInfo.id) {
                    return { ...l, [dragLineInfo.point]: currentPos };
                }
                return l;
            }));
            return;
        }

        if (draggingId === null || isDrawingEnabled) return;

        const tah = tahy[draggingId];
        if (!tah) return;

        const zeroY = ZERO_Y;
        const scale = SCALE_FACTOR;
        const dek = Number(tah.dek) || 0;
        const uva = Number(tah.uva) || 0;

        // DECORATION RESIZE MODE
        if (draggingId === dekAdjustId && resizeMode) {
            if (resizeMode === 'top') {
                // Bottom is fixed (tah.pod), top moves
                let newDek = (zeroY - svgP.y) / scale - tah.pod;
                newDek = Math.max(1, Math.round(newDek));
                onUpdateTah(draggingId, { dek: newDek });
            } else if (resizeMode === 'bottom') {
                // Top is fixed (tah.pod + tah.dek), bottom moves
                const currentTop = tah.pod + dek;
                let newPod = (zeroY - svgP.y) / scale;
                newPod = Math.max(0, Math.min(currentTop - 1, Math.round(newPod)));
                const newDek = currentTop - newPod;
                onUpdateTah(draggingId, { pod: newPod, dek: newDek });
            }
            return;
        }

        // SLING ADJUST MODE: Hook moves, decoration stays put, uva changes
        if (draggingId === slingAdjustId) {
            let newUva = (zeroY - svgP.y) / scale - (tah.pod + dek);
            newUva = Math.max(0, Math.round(newUva));

            onUpdateTah(draggingId, {
                uva: newUva,
                isTopLimit: (tah.pod + dek + newUva) >= stageHeightCm - 1
            });
            return;
        }

        // STANDARD MODE: Everything moves together (pod changes)
        const maxPod = stageHeightCm - (dek + uva);
        const minPod = minHeightCm - (dek + uva);

        // Calculate mouse height from floor in cm
        const mouseHeightCm = (zeroY - svgP.y) / scale;

        // New pod is mouse height - stored offset
        let rawPod = mouseHeightCm - dragOffset;
        let snappedPod = Math.round(rawPod);

        const SNAP_THRESHOLD = 15;
        if (Math.abs(rawPod) < SNAP_THRESHOLD) {
            snappedPod = 0;
        }

        const newPod = Math.max(minPod, Math.min(maxPod, snappedPod));

        onUpdateTah(draggingId, {
            pod: newPod,
            isTopLimit: newPod >= maxPod - 1
        });
    };

    const handleMouseUp = () => {
        setDraggingId(null);
        setSlingAdjustId(null);
        setDekAdjustId(null);
        setResizeMode(null);
        setDragLineInfo(null);
        setDraggingTextId(null);
        setDragOffset(0);
    };

    const handleTextClick = (e: React.MouseEvent) => {
        if (!isDrawingEnabled || drawTool !== 'text') return;

        const svg = e.currentTarget as SVGSVGElement;
        const ctm = svg.getScreenCTM();
        if (!ctm) return;

        const pt = svg.createSVGPoint();
        pt.x = e.clientX;
        pt.y = e.clientY;
        const svgP = pt.matrixTransform(ctm.inverse());

        const newLabel: TextLabel = {
            id: `text-${Date.now()}`,
            pos: { x: svgP.x, y: svgP.y },
            text: 'Klikněte pro úpravu',
            color: '#000000',
            fontSize: 16
        };

        onUpdateTextLabels([...textLabels, newLabel]);
        onSelectText(newLabel.id);
        // Delay opening the edit to avoid immediate closing if handled elsewhere
        setTimeout(() => {
            onTextDoubleClick?.(newLabel.id);
        }, 50);
    };

    const handleSvgClick = (e: React.MouseEvent) => {
        if (!isDrawingEnabled || drawTool !== 'line') return;

        const svg = e.currentTarget as SVGSVGElement;
        const ctm = svg.getScreenCTM();
        if (!ctm) return;

        const pt = svg.createSVGPoint();
        pt.x = e.clientX;
        pt.y = e.clientY;
        const svgP = pt.matrixTransform(ctm.inverse());
        const currentPos = { x: svgP.x, y: svgP.y };

        if (!pendingLine) {
            setPendingLine(currentPos);
        } else {
            const newLine: VectorLine = {
                id: `vline-${Date.now()}`,
                start: pendingLine,
                end: currentPos,
                color: drawingColor,
                lineStyle: defaultLineStyle,
                lineWidth: defaultLineWidth
            };
            onUpdateVectorLines([...vectorLines, newLine]);
            onSelectVector(newLine.id);
            setPendingLine(null);
        }
    };
    const safeTahy = useMemo(() => {
        const next: Record<number, TahState> = {};
        TAH_IDS.forEach(id => {
            next[id] = (tahy && tahy[id]) ? tahy[id] : {
                id, dek: 0, uva: 0, pod: 0,
                isHanging: false, isTopLimit: false, isBottomLimit: false
            };
        });
        return next;
    }, [tahy]);

    return (
        <div
            className="relative bg-white shadow-2xl select-none"
            style={{
                width: IMG_WIDTH,
                height: IMG_HEIGHT,
                maxWidth: '100%',
                maxHeight: 'calc(100vh - 200px)',
                aspectRatio: `${IMG_WIDTH} / ${IMG_HEIGHT}`
            }}
        >
            <img
                src={stageBgImage}
                alt="Stage Blueprint"
                className="absolute inset-0 w-full h-full object-contain opacity-95 pointer-events-none"
            />

            <svg
                className={`absolute inset-0 w-full h-full ${(isDrawingEnabled && drawTool !== 'line' && drawTool !== 'select' && drawTool !== 'text') ? 'pointer-events-none z-0' : 'pointer-events-auto z-20'}`}
                viewBox={`0 0 ${IMG_WIDTH} ${IMG_HEIGHT}`}
                onMouseMove={handleMouseMove}
                onMouseUp={handleMouseUp}
                onMouseLeave={handleMouseUp}
                onMouseDown={(e) => {
                    // Click on background (SVG itself) clears selections
                    if (e.target === e.currentTarget) {
                        setSlingAdjustId(null);
                        setDekAdjustId(null);
                        onSelectVector(null);
                        onSelectText(null);
                    }

                    // Process text creation
                    if (isDrawingEnabled && drawTool === 'text' && e.target === e.currentTarget) {
                        handleTextClick(e);
                        e.stopPropagation();
                    }

                    // Process line drawing if in line mode - regardless of click target
                    if (isDrawingEnabled && drawTool === 'line') {
                        handleSvgClick(e);
                        // Prevent other actions (like drag start) when drawing
                        e.stopPropagation();
                    }
                }}
            >
                {TAH_IDS.map(id => {
                    const tah = safeTahy[id];
                    const hookX = HOIST_HOOK_CENTERS[id]?.x || 0;
                    const x = hookX;

                    const zeroY = ZERO_Y;
                    const scale = SCALE_FACTOR;

                    const dek = Number(tah.dek) || 0;
                    const uva = Number(tah.uva) || 0;
                    const pod = Number(tah.pod) || 0;

                    const bottomY = zeroY - pod * scale;
                    const topOfDekY = bottomY - (dek * scale);
                    const hookY = topOfDekY - (uva * scale);

                    // Klidová pozice háku (přesně na kroužku v nákresu)
                    const baseHookY = HOIST_TOP_Y;

                    const effectiveHookY = tah.isHanging ? hookY : baseHookY;
                    const effectiveTopOfDekY = tah.isHanging ? topOfDekY : baseHookY;

                    const isSelected = selectedId === id;

                    return (
                        <g
                            key={id}
                            className={`group transition-opacity duration-300 ${isDrawingEnabled ? 'opacity-30' : 'opacity-100'}`}
                            onClick={() => !isDrawingEnabled && onSelectTah(id)}
                            onDoubleClick={(e) => {
                                if (isDrawingEnabled) return;
                                e.stopPropagation();
                                setSlingAdjustId(id);
                            }}
                            cursor="pointer"
                        >
                            {/* Větší zóna pro kliknutí kolem tahu */}
                            <rect x={x - 8} y={0} width={16} height={IMG_HEIGHT} fill="transparent" />

                            {/* Lano tahu (z pevné kladky dolů) */}
                            <motion.line
                                x1={x} x2={x} y1={HOIST_TOP_Y} y2={effectiveHookY}
                                animate={{ x1: x, x2: x, y1: HOIST_TOP_Y, y2: effectiveHookY }}
                                stroke={isSelected ? "#3b82f6" : "#222"}
                                strokeWidth={isSelected ? 3 : 2}
                                strokeOpacity={isSelected ? 1 : 0.7}
                            />

                            {/* Hák / Tečka tahu - sedí PŘESNĚ na kroužku v obraze */}
                            <motion.circle
                                cx={x} cy={effectiveHookY}
                                animate={{ cx: x, cy: effectiveHookY }}
                                whileHover={{ r: isSelected ? 8 : 7.5 }}
                                r={isSelected ? 5.5 : 4}
                                fill={isSelected ? "#3b82f6" : "#111"}
                                fillOpacity={0.9}
                                stroke={isSelected ? "#fff" : "none"}
                                strokeWidth={1}
                                onMouseDown={(e: React.MouseEvent) => {
                                    if (isDrawingEnabled) return; // Allow bubbling for lines
                                    e.stopPropagation();
                                    setDraggingId(id);
                                    onSelectTah(id);

                                    // AUTO-HANG: If dragging an empty hook, automatically add a 10cm decoration
                                    if (!tah.isHanging) {
                                        onUpdateTah(id, {
                                            isHanging: true,
                                            dek: 10,
                                            uva: 0,
                                            pod: tah.pod // keep current pod height
                                        });
                                    }
                                }}
                                className="cursor-ns-resize transition-all"
                            />

                            {/* Indikátor režimu úvazku (červené kolečko při dvojkliku) */}
                            {slingAdjustId === id && (
                                <circle
                                    cx={x}
                                    cy={effectiveHookY}
                                    r={18}
                                    fill="rgba(239, 68, 68, 0.1)"
                                    stroke="#ef4444"
                                    strokeWidth={2}
                                    strokeDasharray="3 2"
                                    className="animate-pulse cursor-ns-resize"
                                    onMouseDown={(e) => {
                                        e.stopPropagation();
                                        setDraggingId(id);
                                        onSelectTah(id);
                                    }}
                                />
                            )}

                            {/* Úvazek */}
                            <AnimatePresence>
                                {tah.isHanging && uva > 0 && (
                                    <motion.line
                                        x1={x} x2={x} y1={effectiveHookY} y2={effectiveTopOfDekY}
                                        initial={{ opacity: 0 }}
                                        animate={{ opacity: 1 }}
                                        exit={{ opacity: 0 }}
                                        stroke={isSelected ? "#f87171" : "#ef4444"}
                                        strokeWidth={isSelected ? 3 : 2}
                                    />
                                )}
                            </AnimatePresence>

                            {/* Dekorace */}
                            <AnimatePresence>
                                {tah.isHanging && (
                                    <>
                                        <motion.rect
                                            key={`dek-rect-${id}`}
                                            x={x - DECORATION_HALF_WIDTH}
                                            y={effectiveTopOfDekY}
                                            width={decorationWidth}
                                            height={Math.max(1, dek * SCALE_FACTOR)}
                                            fill={tah.color ? `${tah.color}66` : "rgba(200, 200, 200, 0.4)"}
                                            stroke={tah.color || "#666"}
                                            initial={{ opacity: 0, scaleY: 0 }}
                                            animate={{ opacity: 1, scaleY: 1 }}
                                            exit={{ opacity: 0, scaleY: 0 }}
                                            style={{ originY: 0 }}
                                            whileHover={{
                                                scaleX: 1.1,
                                                fillOpacity: 0.8
                                            }}
                                            onMouseDown={(e: React.MouseEvent) => {
                                                if (isDrawingEnabled) return;
                                                e.stopPropagation();

                                                // Calculate offset in CM relative to pod
                                                const svg = (e.currentTarget as SVGElement).ownerSVGElement;
                                                if (svg) {
                                                    const pt = svg.createSVGPoint();
                                                    pt.x = e.clientX;
                                                    pt.y = e.clientY;
                                                    const svgP = pt.matrixTransform(svg.getScreenCTM()?.inverse());

                                                    // Mouse height from floor in cm
                                                    const mouseHeightCm = (ZERO_Y - svgP.y) / SCALE_FACTOR;
                                                    // Offset = Mouse Height - Current Pod
                                                    setDragOffset(mouseHeightCm - tah.pod);
                                                }

                                                setDraggingId(id);
                                                onSelectTah(id);
                                            }}
                                            onDoubleClick={(e: React.MouseEvent) => {
                                                if (isDrawingEnabled) return;
                                                e.stopPropagation();
                                                setDekAdjustId(id);
                                            }}
                                            className={`${!tah.color ? (isSelected ? 'fill-blue-500/40 stroke-blue-500' : 'fill-zinc-400/30 stroke-zinc-500') : (isSelected ? 'stroke-blue-500' : '')} transition-colors cursor-move`}
                                            strokeWidth={isSelected ? 2.5 : 1.5}
                                            strokeDasharray={dekAdjustId === id ? "4 2" : "none"}
                                        />

                                        {/* Resize Handles - Arrow Indicators (Only in Edit Mode) */}
                                        {dekAdjustId === id && (
                                            <>
                                                {/* TOP ARROW (pointing up) */}
                                                <g
                                                    cursor="ns-resize"
                                                    className="pointer-events-auto"
                                                    onMouseDown={(e) => {
                                                        e.stopPropagation();
                                                        setDraggingId(id);
                                                        setResizeMode('top');
                                                    }}
                                                >
                                                    {/* Arrow background circle */}
                                                    <motion.circle
                                                        cx={x} cy={effectiveTopOfDekY - 12}
                                                        animate={{ cx: x, cy: effectiveTopOfDekY - 12 }}
                                                        r={10}
                                                        fill="rgba(59, 130, 246, 0.9)"
                                                        stroke="#fff"
                                                        strokeWidth={2}
                                                    />
                                                    {/* Arrow pointing up */}
                                                    <motion.polygon
                                                        points={`${x},${effectiveTopOfDekY - 16} ${x - 4},${effectiveTopOfDekY - 10} ${x + 4},${effectiveTopOfDekY - 10}`}
                                                        animate={{
                                                            points: `${x},${effectiveTopOfDekY - 16} ${x - 4},${effectiveTopOfDekY - 10} ${x + 4},${effectiveTopOfDekY - 10}`
                                                        }}
                                                        fill="#fff"
                                                    />
                                                    {/* Arrow stem */}
                                                    <motion.rect
                                                        x={x - 1.5}
                                                        y={effectiveTopOfDekY - 10}
                                                        width={3}
                                                        height={6}
                                                        animate={{
                                                            x: x - 1.5,
                                                            y: effectiveTopOfDekY - 10,
                                                            width: 3,
                                                            height: 6
                                                        }}
                                                        fill="#fff"
                                                    />
                                                </g>

                                                {/* BOTTOM ARROW (pointing down) */}
                                                <g
                                                    cursor="ns-resize"
                                                    className="pointer-events-auto"
                                                    onMouseDown={(e) => {
                                                        e.stopPropagation();
                                                        setDraggingId(id);
                                                        setResizeMode('bottom');
                                                    }}
                                                >
                                                    {/* Arrow background circle */}
                                                    <motion.circle
                                                        cx={x} cy={bottomY + 12}
                                                        animate={{ cx: x, cy: bottomY + 12 }}
                                                        r={10}
                                                        fill="rgba(59, 130, 246, 0.9)"
                                                        stroke="#fff"
                                                        strokeWidth={2}
                                                    />
                                                    {/* Arrow pointing down */}
                                                    <motion.polygon
                                                        points={`${x},${bottomY + 16} ${x - 4},${bottomY + 10} ${x + 4},${bottomY + 10}`}
                                                        animate={{
                                                            points: `${x},${bottomY + 16} ${x - 4},${bottomY + 10} ${x + 4},${bottomY + 10}`
                                                        }}
                                                        fill="#fff"
                                                    />
                                                    {/* Arrow stem */}
                                                    <motion.rect
                                                        x={x - 1.5}
                                                        y={bottomY + 4}
                                                        width={3}
                                                        height={6}
                                                        animate={{
                                                            x: x - 1.5,
                                                            y: bottomY + 4,
                                                            width: 3,
                                                            height: 6
                                                        }}
                                                        fill="#fff"
                                                    />
                                                </g>
                                            </>
                                        )}
                                    </>
                                )}
                            </AnimatePresence>

                            {/* Horní úvrať */}
                            {tah.isTopLimit && (
                                <line
                                    x1={x - 10} y1={HOIST_TOP_Y + 5}
                                    x2={x + 10} y2={HOIST_TOP_Y + 5}
                                    stroke="#f59e0b" strokeWidth={3}
                                    strokeLinecap="round"
                                />
                            )}

                            {/* Dolní úvrať (Sjezd na podlahu 0cm) */}
                            {tah.isBottomLimit && (
                                <line
                                    x1={x - 10} y1={zeroLevelY}
                                    x2={x + 10} y2={zeroLevelY}
                                    stroke="#3b82f6" strokeWidth={3}
                                    strokeLinecap="round"
                                />
                            )}

                            {/* ID popisek - ukotven k pevné kladce (y: 43) */}
                            <line
                                x1={x}
                                y1={topLimitY}
                                x2={x}
                                y2={hookY}
                                stroke={isSelected ? "#3b82f6" : "#666"}
                                strokeWidth={isSelected ? 2 : 1.2}
                            />
                            <rect
                                x={id < 10 ? x - 7 : x - 9}
                                y={HOIST_TOP_Y - 37}
                                width={id < 10 ? 14 : 18}
                                height={13}
                                rx="3"
                                fill="white"
                                stroke={isSelected ? "#3b82f6" : "#e5e7eb"}
                                strokeWidth={0.5}
                            />
                            <text
                                x={x}
                                y={HOIST_TOP_Y - 27}
                                textAnchor="middle"
                                className={`text-[10px] font-black select-none ${isSelected ? 'fill-blue-600' : 'fill-zinc-950'}`}
                            >
                                {id}
                            </text>
                        </g>
                    );
                })}

                {/* Vector Lines Layer */}
                <g id="vector-lines">
                    {vectorLines.map(line => {
                        const isSelected = selectedVectorId === line.id;
                        const strokeWidth = line.lineWidth || 2;

                        let dashArray = "none";
                        if (line.lineStyle === 'dashed') dashArray = `${strokeWidth * 4},${strokeWidth * 3}`;
                        if (line.lineStyle === 'dotted') dashArray = `${strokeWidth},${strokeWidth * 2}`;

                        return (
                            <g
                                key={line.id}
                                onDoubleClick={(e) => {
                                    if (showVectorHandles) {
                                        e.stopPropagation();
                                        onLineDoubleClick?.(line.id);
                                    }
                                }}
                            >
                                {/* Invisible larger hit area for easier clicking */}
                                <line
                                    x1={line.start.x}
                                    y1={line.start.y}
                                    x2={line.end.x}
                                    y2={line.end.y}
                                    stroke="transparent"
                                    strokeWidth={20}
                                    className={showVectorHandles ? "cursor-pointer" : "pointer-events-none"}
                                    onClick={(e) => {
                                        if (showVectorHandles) {
                                            e.stopPropagation();
                                            onSelectVector(line.id);
                                        }
                                    }}
                                />
                                <line
                                    x1={line.start.x}
                                    y1={line.start.y}
                                    x2={line.end.x}
                                    y2={line.end.y}
                                    stroke={line.color}
                                    strokeWidth={isSelected ? strokeWidth + 2 : strokeWidth}
                                    strokeDasharray={dashArray}
                                    strokeLinecap={line.lineStyle === 'dotted' ? 'round' : 'butt'}
                                    className={showVectorHandles ? "cursor-pointer" : "pointer-events-none"}
                                    onClick={(e) => {
                                        if (showVectorHandles) {
                                            e.stopPropagation();
                                            onSelectVector(line.id);
                                        }
                                    }}
                                />
                                {/* Drag Handles - Only visible in SELECT mode */}
                                {showVectorHandles && (
                                    <>
                                        <circle
                                            cx={line.start.x}
                                            cy={line.start.y}
                                            r={isSelected ? 8 : 6}
                                            fill="white"
                                            stroke={line.color}
                                            strokeWidth={isSelected ? 3 : 2}
                                            className="cursor-move hover:fill-blue-100"
                                            onMouseDown={(e) => {
                                                e.stopPropagation();
                                                onSelectVector(line.id);
                                                setDragLineInfo({ id: line.id, point: 'start' });
                                            }}
                                            onDoubleClick={(e) => {
                                                e.stopPropagation();
                                                onUpdateVectorLines(vectorLines.filter(l => l.id !== line.id));
                                                if (isSelected) onSelectVector(null);
                                            }}
                                        />
                                        <circle
                                            cx={line.end.x}
                                            cy={line.end.y}
                                            r={isSelected ? 8 : 6}
                                            fill="white"
                                            stroke={line.color}
                                            strokeWidth={isSelected ? 3 : 2}
                                            className="cursor-move hover:fill-blue-100"
                                            onMouseDown={(e) => {
                                                e.stopPropagation();
                                                onSelectVector(line.id);
                                                setDragLineInfo({ id: line.id, point: 'end' });
                                            }}
                                            onDoubleClick={(e) => {
                                                e.stopPropagation();
                                                onUpdateVectorLines(vectorLines.filter(l => l.id !== line.id));
                                                if (isSelected) onSelectVector(null);
                                            }}
                                        />
                                    </>
                                )}
                            </g>
                        );
                    })}

                    {/* Text Labels Layer */}
                    <g id="text-labels">
                        {textLabels.map(label => {
                            const isSelected = selectedTextId === label.id;
                            const textWidth = label.text.length * (label.fontSize * 0.6);
                            const textHeight = label.fontSize;

                            return (
                                <g
                                    key={label.id}
                                    onDoubleClick={(e) => {
                                        if (showVectorHandles) {
                                            e.stopPropagation();
                                            onTextDoubleClick?.(label.id);
                                        }
                                    }}
                                    onMouseDown={(e) => {
                                        if (showVectorHandles) {
                                            e.stopPropagation();
                                            onSelectText(label.id);
                                            // Deselect other things
                                            onSelectVector(null);
                                            onSelectTah(-1);

                                            // Start dragging
                                            const svg = (e.currentTarget as SVGElement).ownerSVGElement;
                                            if (svg) {
                                                const pt = svg.createSVGPoint();
                                                pt.x = e.clientX;
                                                pt.y = e.clientY;
                                                const svgP = pt.matrixTransform(svg.getScreenCTM()?.inverse());
                                                setDraggingTextId(label.id);
                                                setTextDragOffset({
                                                    x: svgP.x - label.pos.x,
                                                    y: svgP.y - label.pos.y
                                                });
                                            }
                                        }
                                    }}
                                    className={showVectorHandles ? "cursor-move" : "pointer-events-none"}
                                >
                                    {/* Background Rect */}
                                    {label.backgroundColor && (
                                        <rect
                                            x={label.pos.x - 6}
                                            y={label.pos.y - textHeight + 2}
                                            width={textWidth + 12}
                                            height={textHeight + 10}
                                            fill={label.backgroundColor}
                                            rx={4}
                                        />
                                    )}
                                    {/* Hit area (if no background, still need something to click) */}
                                    {!label.backgroundColor && (
                                        <rect
                                            x={label.pos.x - 5}
                                            y={label.pos.y - textHeight}
                                            width={textWidth + 10}
                                            height={textHeight + 10}
                                            fill="transparent"
                                        />
                                    )}
                                    {isSelected && (
                                        <rect
                                            x={label.pos.x - 8}
                                            y={label.pos.y - textHeight - 2}
                                            width={textWidth + 16}
                                            height={textHeight + 16}
                                            fill="none"
                                            stroke="#3b82f6"
                                            strokeWidth={1.5}
                                            strokeDasharray="4,2"
                                            rx={4}
                                        />
                                    )}
                                    <text
                                        x={label.pos.x}
                                        y={label.pos.y}
                                        fill={label.color}
                                        fontSize={label.fontSize}
                                        fontWeight="bold"
                                        style={{ filter: isSelected ? 'drop-shadow(0 0 4px rgba(59, 130, 246, 0.5))' : 'none' }}
                                        className="select-none"
                                    >
                                        {label.text}
                                    </text>
                                </g>
                            );
                        })}
                    </g>

                    {/* Drawing Preview */}
                    {pendingLine && (
                        <g>
                            <line
                                x1={pendingLine.x}
                                y1={pendingLine.y}
                                x2={mousePos.x}
                                y2={mousePos.y}
                                stroke={drawingColor}
                                strokeWidth={2}
                                strokeDasharray="5,5"
                                opacity={0.6}
                            />
                            <circle cx={pendingLine.x} cy={pendingLine.y} r={4} fill={drawingColor} />
                        </g>
                    )}
                </g>
            </svg>

            <DrawingLayer
                width={IMG_WIDTH}
                height={IMG_HEIGHT}
                color={drawingColor}
                tool={drawTool}
                enabled={isDrawingEnabled}
            />

            {/* Zobrazení nuly (Podlahy) */}
            <div className="absolute left-0 w-full h-[1px] bg-blue-500/20 pointer-events-none" style={{ top: ZERO_Y }} />

            {/* Zobrazení Minimální výšky (Červená ryska) */}
            <div className="absolute left-0 w-full h-[1px] bg-red-600/30 pointer-events-none border-t border-dashed border-red-600/50"
                style={{ top: zeroLevelY - minHeightCm * scaleConfig }} />

            {/* Spotlight efekt při nastavování pixelů */}
            <AnimatePresence>
                {highlightY !== null && highlightY !== undefined && (
                    <motion.div
                        initial={{ opacity: 0 }}
                        animate={{ opacity: 1 }}
                        exit={{ opacity: 0 }}
                        className="absolute inset-0 z-[60] pointer-events-none overflow-hidden"
                        style={{
                            backgroundColor: 'rgba(0,0,0,0.6)',
                            maskImage: `linear-gradient(to bottom, 
                                black 0%, 
                                black ${highlightY - 120}px, 
                                transparent ${highlightY - 100}px, 
                                transparent ${highlightY + 100}px, 
                                black ${highlightY + 120}px, 
                                black 100%)`,
                            WebkitMaskImage: `linear-gradient(to bottom, 
                                black 0%, 
                                black ${highlightY - 120}px, 
                                transparent ${highlightY - 100}px, 
                                transparent ${highlightY + 100}px, 
                                black ${highlightY + 120}px, 
                                black 100%)`
                        }}
                    >
                        <div className="absolute left-0 w-full h-[200px] border-y border-white/20 bg-white/5"
                            style={{ top: highlightY - 100 }} />
                    </motion.div>
                )}
            </AnimatePresence>
        </div >
    );
};

export default StageCanvas;
