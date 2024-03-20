namespace Mover.Shared;
public static class Constants
{
    public static class Options
    {
        public const string SecretsStore = "secretsStore";
        public const string Jwt_Issuer = "Jwt:Issuer";
        public const string Jwt_Audience = "Jwt:Audience";
    }

    public static class Secrets
    {
        public const string MoverDbConnString = "moverDbConnString";
        public const string MoverSqlConnString = "moverSqlConnString";
        public const string MoverJwtKey = "jwtKey";
    }

    public static class Dapr
    {
        public const string MOVER_PUBSUB = "mover-pubsub";
        public const string MOVER_FITBLOB = "mover-fitblob";
        public const string MOVER_GPXBLOB = "mover-gpxblob";
        public const string MOVER_RAWBLOB = "mover-rawblob";
    }

    public static class Extension
    {
        public const string FIT = ".fit";
        public const string GPX = ".gpx";
    }
}
