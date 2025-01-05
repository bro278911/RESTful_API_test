using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// �[�J���n���A��
builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();
// Add services to the container.
// ���J�t�m
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*", // �Ҧ����|
            Limit = 5,      // �C�Ӯɶ����f���\���̤j�ШD��
            Period = "10s"  // �ɶ����f���ס]10��^
        }
    };
});
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// �M�� IP �t�v������n��
app.UseIpRateLimiting();

app.MapControllers();

app.Run();
