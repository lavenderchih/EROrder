namespace EROrder.Shared.Helpers
{
    public static class MagicHelper
    {
        /// <summary>API 版本常數</summary>
        public static class ApiVersions
        {
            public const string V1 = "v1";
            /// <summary>當前預設版本</summary>
            public const string Current = V1;
            /// <summary>所有支援的版本</summary>
            public static readonly string[] SupportedVersions = [V1];
        }
        
        /// <summary>API 路由常數</summary>
        public static class ApiRoutes
        {
            public const string BaseApi = "api";
        }
        
        /// <summary>Swagger 文件資訊</summary>
        public static class SwaggerInfo
        {
            public const string Title = "EROrder API Documentation";
            public const string Description = "Web API reference and endpoints documentation.";
            public static string GetVersionTitle(string version) => $"{Title} {version}";
        }
    }
}
