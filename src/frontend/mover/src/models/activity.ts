export type activity = {
    idRaw: string;
    activityType: string;
    timestamp: number;
    tTime: number;
    tPDrop: number;
    tDistance: number;
};

export type activityFull = activity & {
    fn: string;
    points: point[];
};

export type point = {
    ts: number;
    temp: number;
    ele: number;
    s: number;
    hr: number;
    c: number;
};
