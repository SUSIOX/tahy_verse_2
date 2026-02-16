export interface TahState {
    id: number;
    dek: number; // výška dekorace (cm)
    uva: number; // délka úvazku (cm)
    pod: number; // výška od podlahy (cm)
    isHanging: boolean;
    isTopLimit: boolean;
    isBottomLimit: boolean;
    color?: string; // Barva kulisy
    name?: string;  // Název/Popis
}

export interface StageConfig {
    stageHeightCm: number; // Vzdálenost podlaha -> kladky (cm)
    topLimitY: number;     // Horní úvrať / Kladky (px)
    minHeightCm: number;   // Limit háku nad podlahou (cm)
    zeroLevelY: number;    // Pixelová pozice podlahy (0 cm)
    scale: number;        // Měřítko (px/cm)
    decorationWidth: number; // Šířka kulisy (px)
}

export interface Point {
    x: number;
    y: number;
}

export type LineStyle = 'solid' | 'dashed' | 'dotted';

export interface VectorLine {
    id: string;
    start: Point;
    end: Point;
    color: string;
    lineWidth?: number;
    lineStyle?: LineStyle;
}

export interface TextLabel {
    id: string;
    pos: Point;
    text: string;
    color: string;
    fontSize: number;
    width?: number;
    backgroundColor?: string;
    backgroundOpacity?: number;
}

export interface Scene {
    id: string;
    name: string;
    tahy: Record<number, TahState>;
    vectorLines?: VectorLine[];
    textLabels?: TextLabel[];
}

export const TAH_IDS = [1, 2, 3, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15, 17, 18];

export const DEFAULT_STAGE_CONFIG: StageConfig = {
    stageHeightCm: 900,
    topLimitY: 43,
    minHeightCm: 45,
    zeroLevelY: 482,
    scale: 0.4878,
    decorationWidth: 18
};

export const MAX_STAGE_HEIGHT = 914; // cm
