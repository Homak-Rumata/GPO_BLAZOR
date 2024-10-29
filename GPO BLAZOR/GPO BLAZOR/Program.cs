using GPO_BLAZOR.Client.Class.Date;
using GPO_BLAZOR.Components;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Components.Authorization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GPO_BLAZOR.Client.Class.JSRunTimeAccess;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml;
using System.Xml.Serialization;
using System;
using GPO_BLAZOR.DBAgents;
using Microsoft.AspNetCore.Identity;
using GPO_BLAZOR.DBAgents;
using GPO_BLAZOR.DBAgents.DBModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using GPO_BLAZOR.API_Functions;

namespace GPO_BLAZOR
{


    class Date
    {
        public Guid token { get; set; }
        public string jwt { get; set; }
        public string role { get; set; }
    }

    record FieldDateContainer
    {

        public FieldDateContainer (string name, string type, string id, string text, bool disabled)
        {
            Name = name;
            ClassType = type;
            Id = id;
            Text = text;
            IsDisabled = disabled;
        }

        public string Name { get; set; }
        public string ClassType { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }

        public bool IsDisabled { get; init; }

    }

    class BlockDateContainer
    {
        public BlockDateContainer(string blockname, IEnumerable<FieldDateContainer> date)
        {
            BlockName = blockname;
            Date = date.ToArray();
        }

        public string BlockName { get; set; }
        public FieldDateContainer[] Date { get; set; }
    }

    class PageDateContainer
    {
        public PageDateContainer(string pagename, IEnumerable<BlockDateContainer> date)
        {
            PageName = pagename;
            Date = date.ToArray();
        }

        public string PageName { get; set; }
        public BlockDateContainer[] Date { get; set; }
    }

    class StatmenDateContainer
    {
        public StatmenDateContainer(IEnumerable<PageDateContainer> date)
        {
            //StatmenName = statmenname;
            Date = date.ToArray();
        }

        //public string StatmenName { get; set; }
        public PageDateContainer[] Date { get; set; }
    }

    static class StatmenDate
    {
        static public StatmenDateContainer DefaultInfo = new StatmenDateContainer (
            new PageDateContainer[]{new PageDateContainer("���������������� ������",
                new BlockDateContainer[]{
                    new BlockDateContainer("�������� ������",
                         new FieldDateContainer[]{
                            new FieldDateContainer("FirstNameTextField", "InputInformationField", "FirstNameTextField", "���", false),
                            new FieldDateContainer("SecondNameTextField", "InputInformationField", "SecondNameTextField", "�������", false),
                            new FieldDateContainer("TreeNameTextField", "InputInformationField", "TreeNameTextField", "�������� (��� �������)", false)
                        }),
                    new BlockDateContainer("",
                        new FieldDateContainer[] {
                            new FieldDateContainer("Grp", "CollectionInformationField", "Grp", "������", false),
                            new FieldDateContainer("Direction", "CollectionInformationField", "Direction", "����������� ����������", false)
                        }),
                    new BlockDateContainer("��������",
                        new FieldDateContainer[] {
                            new FieldDateContainer("PracticeSort", "CollectionInformationField", "PracticeSort", "��� ��������", false),
                            new FieldDateContainer("PracticeType", "CollectionInformationField", "PracticeType", "��� ��������", false),
                            new FieldDateContainer("date", "TymeInformationField", "date", "���� ������ ���������", true)
                        }),
                    }
            ),
                new PageDateContainer("������������",
                new BlockDateContainer[]{
                    new BlockDateContainer("������������ ��������",
                        new FieldDateContainer[]{
                            new FieldDateContainer("WorkLeaderFirstNameTextField", "InputInformationField", "WorkLeaderFirstNameTextField", "���", false),
                            new FieldDateContainer("WorkLeaderSecondNameTextField", "InputInformationField", "WorkLeaderSecondNameTextField", "�������", false),
                            new FieldDateContainer("WorkLeaderTreeNameTextField", "InputInformationField", "WorkLeaderTreeNameTextField", "�������� (��� �������)", false)
                        }),
                    new BlockDateContainer("���������� ������",
                        new FieldDateContainer[] {
                            new FieldDateContainer("CafedralLeaderFirstNameTextField", "CollectionInformationField", "CafedralLeaderFirstNameTextField", "���", false),
                            new FieldDateContainer("CafedralLeaderSecondNameTextField", "CollectionInformationField", "CafedralLeaderSecondNameTextField", "�������", false),
                            new FieldDateContainer("CafedralLeaderTreeNameTextField", "CollectionInformationField", "CafedralLeaderTreeNameTextField", "��������", false)
                        }),
                    }
            ),
                new PageDateContainer("�����������",
                 new BlockDateContainer[]{
                    new BlockDateContainer("��������� �����������",
                        new FieldDateContainer[] {
                            new FieldDateContainer("FactoryNameTextField", "InputInformationField", "FactoryNameTextField", "������������", false)
                        }),
                    new BlockDateContainer("������",
                        new FieldDateContainer[] {
                            new FieldDateContainer("RegionNameTextField", "CollectionInformationField", "RegionNameTextField", "�������", false),
                            new FieldDateContainer("DistrictNameTextField", "CollectionInformationField", "DistrictNameTextField", "�����", false),
                            new FieldDateContainer("LocalityNameTextField", "TymeInformationField", "LocalityNameTextField", "��������� �����", false)
                        }),
                    new BlockDateContainer("",
                        new FieldDateContainer[] {
                            new FieldDateContainer("StreetTextField", "CollectionInformationField", "StreetTextField", "�����", false),
                            new FieldDateContainer("buildingNumberTextField", "CollectionInformationField", "buildingNumberTextField", "��������", false),
                            new FieldDateContainer("MailPostNumberTextField", "NumberInformationField", "MailPostNumberTextField", "����� �����", false)
                        })
                    }
            )
        });
    }

    class tempc
    {
        public tempc(string id, string Time, int val1, int val2)
        {
            this.id = id;
            this.Time = DateTime.Now.AddHours(2);
            this.State = val1;
            this.PracticType = val2;
        }

        public string id { get; set; }
        public DateTime Time { get; set; }
        public int State { get; set; }
        public int PracticType { get; set; }
    }

    
    record AutorizationDate
    {
        public string login { get; init; }
        public string Password { get; init; }
    }

    class ErrorMessage
    {
        public string messege { get; set; }
    }

    /// <summary>
    /// ���������
    /// </summary>
    public static class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // �������� ������
        public const string AUDIENCE = "MyAuthClient"; // ����������� ������
        const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ���� ��� ��������
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
    //

    public class Program
    {
        static Dictionary<string, List<string>> SpecialArray = new Dictionary<string, List<string>>()
        {
            {"Grp", new List<string>() {"711-1", "721-1", "731-1", "731-2", "741-1", "761-1" } } ,
            {"Direction", new List<string>(){"�������������� ������������", "7������������ ������������������ ������", "������������ ������������������� ������", "������������� ������������", "������������� ������������" } },
            {"PracticeSort", new List<string>(){"����������������", "��������������" } },
            {"PracticeType", new List<string>(){"����������������"} }
        };

        public static void Main(string[] args)
        {
            Console.Write("Data Base Password: ");
            Gpo2Context cntx = new Gpo2Context(Console.ReadLine());
            
            
           // DBConnector.F(null);

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<CookieStorageAccessor>();
            builder.Services.AddScoped<LocalStorageAccessor>();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddControllers()
                .AddXmlSerializerFormatters();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            builder.Services.AddScoped<AuthenticationStateProvider, IdentetyAuthenticationStateProvider>();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });



            //builder.Services.AddAuthorizationCore();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            List<tempc> b = new List<tempc>()
            {
                new tempc(1.ToString(), 1.ToString(), 1, 1),
                new tempc(2.ToString(), 2.ToString(), 1, 1),
                new tempc(3.ToString(), 3.ToString(), 1, 1)
            };

            Dictionary<string, Dictionary<string, string>> temp = new Dictionary<string, Dictionary<string, string>>()
            {
                {"1", new Dictionary<string, string>() { {"id", "1" }, {"Template", "based" } } },
                {"2", new Dictionary<string, string>() { {"id", "2" }, { "Template", "based" } } },
                {"3", new Dictionary<string, string>() { {"id", "3" }, { "Template", "based" } } }
            };

            XmlDocument fgh = new XmlDocument();
            fgh.LoadXml("<reply success=\"true\">More nodes go here</reply>");



            bool AddOnDictionary<Key, Value>(Dictionary<Key, Value> dictionary, KeyValuePair<Key, Value> value)
            {
                if (dictionary.TryAdd(value.Key, value.Value))
                    return true;
                else
                    dictionary[value.Key] = value.Value;
                return true;
            }

            int calculator = 0;



            app.UseSession();
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseAntiforgery();
            
            

            app.MapGet("/GetAtributes/{Field}", (string Field) => SpecialArray[Field]);
            app.MapGet("/GetAtributes", () => new string[] { "A", "�", "�" });

            app.MapPost("/autorization", (Autorization.AutorizationDate date) =>
            {
                try
                {

                    if (!(Autorization.checkuser(date, cntx).Result))
                    {
                        return Results.Problem("not login or password", "nonautorization", 401, "bad login or password)", "nontype", new Dictionary<string, object> { { "messege", "bad login or password" } });
                    }

                    app.Logger.LogInformation($"User loging: {date.login}");

                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, date.login), new Claim(ClaimTypes.Role, "student") };
                    var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            audience: AuthOptions.AUDIENCE,
                            claims: claims,
                            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // ����� �������� 2 ������
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));



                    return Results.Json(new Date(){
                        token = (Guid.NewGuid()),
                        jwt = new JwtSecurityTokenHandler().WriteToken(jwt), 
                        role = "student" 
                    });
                    /*
                    return !(API_Functions.Autorization.checkuser(date, cntx).Result) ?
                    Results.Problem("not login or password", "nonautorization", 401, "bad login or password)", "nontype", new Dictionary<string, object> { { "messege", "bad login or password"} }) :
                    Results.Json(new Date() { token = (Guid.NewGuid()), role = "student" });*/
                }
                catch (Exception ex)
                {
                    app.Logger.LogError($"User: {date.login}\nError: {ex.Message}");
                    return Results.Problem();
                }
            });

            app.Map("/newJWT", (HttpContext a) =>
            {
                app.Logger.LogInformation("ResponceJWT");
                var o = a.User.Identity;
                if (o is not null && o.IsAuthenticated)
                {
                    var claims = a.User.Claims;
                    foreach (var i in claims)
                    {
                        app.Logger.LogDebug("claim " + i.Value +i.ValueType+" "+i.Type+" "+i.Subject+" ");
                    }
                    var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            claims: claims,
                            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // ����� �������� 2 ������
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    app.Logger.LogInformation($"User: {a.User.Identity.Name} \nnewJWT: {jwt}");
                    return Results.Json(new { jwt = new JwtSecurityTokenHandler().WriteToken(jwt) });
                }
                app.Logger.LogError($"Error new JWT: {a.User.Identity.Name} "+o+" "+o.IsAuthenticated );
                return Results.NotFound();

                
            });

            app.MapGet("/getstatmens/user:{Token}",[Authorize]()=>b);
            app.MapGet("/getformDate:{ID}", [Authorize] (string ID) => { app.Logger.LogInformation($"{ID}: {temp[ID]}"); return temp[ID]; });
            app.MapPost("/getInfo", (Dictionary<string, string> x)=>
            {
                Console.WriteLine("------------------------------------------------");
                string accamulator = "";

                if (temp.TryAdd(x["id"], new Dictionary<string, string>()))
                {
                    app.Logger.LogError($"AddedStatmen: {x["id"]}");
                }

                string id = x["id"];

                x.Remove("id");

                foreach (var item in x)
                {
                     accamulator+=$"{item.Key}: {(item.Value==null||item.Value==("")?("none"):item.Value)}: {AddOnDictionary(temp[id], item)}\n";
                    //Console.WriteLine($"{item.Key} ^ {item.Value} - WriteLine");
                }
                    app.Logger.LogInformation((new EventId(calculator++, "getInfo")), accamulator);
                    return Results.Ok("sucsefull");
            });
            app.MapGet("/getTepmlate/{TeplateName}", [Authorize](string TeplateName) => (StatmenDate.DefaultInfo));


            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }

    public class IdentetyAuthenticationStateProvider: AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState( new System.Security.Claims.ClaimsPrincipal() );
        }
    }
}
