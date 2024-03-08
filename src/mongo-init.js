db.createUser({
    user: "amaia",
    pwd: "password",
    roles: [
        {
            role: "readWrite",
            db: "mover",
        },
    ],
});

db.createCollection("activities");
