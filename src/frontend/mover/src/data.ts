export const menu = [
    {
        id: 1,
        listItems: [
            {
                id: 1,
                title: "Attività",
                url: "/",
                icon: "excercising-man.svg",
            },
        ],
    },
];

export const activityTypes = new Map<string, any>();
activityTypes.set("road", {
    title: "Giro in bici da corsa",
    icon: "/road.svg",
});
activityTypes.set("road_biking", {
    title: "Giro in bici da corsa",
    icon: "/road.svg",
});
activityTypes.set("ebikemountain", {
    title: "Giro in e-mtb",
    icon: "/ebike.svg",
});
activityTypes.set("mountain", {
    title: "Giro in mtb",
    icon: "/mtb.svg",
});

export const activities = [
    {
        idRaw: "sss",
        activityType: "road",
        timestamp: 1710157550,
        tTime: 3600,
        tPDrop: 1100,
        tDistance: 56,
    },
    {
        idRaw: "aaa",
        activityType: "ebikemountain",
        timestamp: 1710157550,
        tTime: 3600,
        tPDrop: 1100,
        tDistance: 56,
    },
    {
        idRaw: "bbb",
        activityType: "mountain",
        timestamp: 1710157550,
        tTime: 3600,
        tPDrop: 1100,
        tDistance: 56,
    },
];
