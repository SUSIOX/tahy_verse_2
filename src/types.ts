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
    nosnost?: number; // Nosnost tahu (kg)
    funkce?: 'LOCK' | 'světla' | 'kulisy'; // Funkce tahu
}

export interface StageConfig {
    stageHeightCm: number; // Vzdálenost podlaha -> kladky (cm)
    topLimitY: number;     // Horní úvrať / Kladky (px)
    minHeightCm: number;   // Limit háku nad podlahou (cm)
    zeroLevelY: number;    // Pixelová pozice podlahy (0 cm)
    scale: number;        // Měřítko (px/cm)
    decorationWidth: number; // Šířka kulisy (px)
    customBgImage?: string; // Volitelný podkladový obrázek (base64 nebo URL)
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

export const TAH_IDS = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];

export interface Space {
    id: string;
    name: string;
    productionName: string;
    activeSceneId: string;
    scenes: Scene[];
    stageConfig: StageConfig;
    hoistPositions: HoistRegistry;
}

export const DEFAULT_STAGE_CONFIG: StageConfig = {
    stageHeightCm: 900,
    topLimitY: 120.9,
    minHeightCm: 45,
    zeroLevelY: 540,
    scale: 0.4656,
    decorationWidth: 18
};

export const MAX_STAGE_HEIGHT = 914; // cm

export type HoistRegistry = Record<number, { x: number }>;

export const DEFAULT_HOIST_POSITIONS: HoistRegistry = {
    1: { x: 699.2 },
    2: { x: 712 },
    3: { x: 726 },
    4: { x: 744 },
    5: { x: 761 },
    6: { x: 775 },
    7: { x: 790 },
    8: { x: 803 },
    9: { x: 817 },
    10: { x: 830 },
    11: { x: 846 },
    12: { x: 862 },
    13: { x: 874 },
    14: { x: 888 },
    15: { x: 924 },
    16: { x: 937 }, // Opraveno (původně 1149.2 mimo obraz)
    17: { x: 950 },
    18: { x: 963 }
};
