import React, { useRef, useState, useCallback } from 'react';

interface Props {
    width: number;
    height: number;
    color: string;
    tool: 'pen' | 'eraser' | 'line' | 'select';
    enabled: boolean;
}

const DrawingLayer: React.FC<Props> = ({ width, height, color, tool, enabled }: Props) => {
    const canvasRef = useRef<HTMLCanvasElement>(null);
    const [isDrawing, setIsDrawing] = useState(false);

    const getCoordinates = useCallback((e: React.MouseEvent) => {
        const canvas = canvasRef.current;
        if (!canvas) return { x: 0, y: 0 };

        const rect = canvas.getBoundingClientRect();
        const scaleX = canvas.width / rect.width;
        const scaleY = canvas.height / rect.height;

        return {
            x: (e.clientX - rect.left) * scaleX,
            y: (e.clientY - rect.top) * scaleY
        };
    }, []);

    const startDrawing = (e: React.MouseEvent) => {
        // Only active for raster tools: pen or eraser
        if (!enabled || tool === 'line' || tool === 'select') return;
        const canvas = canvasRef.current;
        if (!canvas) return;
        const ctx = canvas.getContext('2d');
        if (!ctx) return;

        const { x, y } = getCoordinates(e);
        ctx.beginPath();
        ctx.moveTo(x, y);
        setIsDrawing(true);
    };

    const draw = (e: React.MouseEvent) => {
        if (!isDrawing || !enabled || tool === 'line' || tool === 'select') return;
        const canvas = canvasRef.current;
        if (!canvas) return;
        const ctx = canvas.getContext('2d');
        if (!ctx) return;

        const { x, y } = getCoordinates(e);
        const isEraser = tool === 'eraser';

        ctx.globalCompositeOperation = isEraser ? 'destination-out' : 'source-over';
        ctx.lineTo(x, y);
        ctx.strokeStyle = color;
        ctx.lineWidth = isEraser ? 20 : 3;
        ctx.lineCap = 'round';
        ctx.lineJoin = 'round';
        ctx.stroke();
    };

    const stopDrawing = () => {
        setIsDrawing(false);
    };

    return (
        <canvas
            ref={canvasRef}
            width={width}
            height={height}
            onMouseDown={startDrawing}
            onMouseMove={draw}
            onMouseUp={stopDrawing}
            onMouseOut={stopDrawing}
            className={`absolute inset-0 z-10 transition-opacity ${enabled && tool !== 'line' && tool !== 'select' ? 'pointer-events-auto cursor-crosshair opacity-100' : 'pointer-events-none opacity-80'}`}
        />
    );
};

export default DrawingLayer;
