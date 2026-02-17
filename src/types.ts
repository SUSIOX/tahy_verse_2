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
    topLimitY: 32.98,
    minHeightCm: 45,
    zeroLevelY: 482,
    scale: 5.93,
    decorationWidth: 18
};

export const MAX_STAGE_HEIGHT = 914; // cm

export type HoistRegistry = Record<number, { x: number }>;

export const DEFAULT_HOIST_POSITIONS: HoistRegistry = {
    1: { x: 699.2 },
    2: { x: 729.2 },
    3: { x: 759.2 },
    4: { x: 789.2 },
    5: { x: 819.2 },
    6: { x: 849.2 },
    7: { x: 879.2 },
    8: { x: 909.2 },
    9: { x: 939.2 },
    10: { x: 969.2 },
    11: { x: 999.2 },
    12: { x: 1029.2 },
    13: { x: 1059.2 },
    14: { x: 1089.2 },
    15: { x: 1119.2 },
    16: { x: 1149.2 },
    17: { x: 1179.2 },
    18: { x: 1209.2 }
};
