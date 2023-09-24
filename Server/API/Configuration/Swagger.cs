namespace API.Configuration
{
    public static class Swagger
    {
        public static WebApplication ConfigureSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }
    }
}
