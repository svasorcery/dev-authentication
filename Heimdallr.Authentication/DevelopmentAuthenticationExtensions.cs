using Heimdallr.Authentication;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BasicAuthenticationAppBuilderExtensions
    {
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder)
            => builder.AddBasic(DevelopmentAuthenticationDefaults.AuthenticationScheme);

        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddBasic(authenticationScheme, configureOptions: null);

        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, Action<DevelopmentAuthenticationOptions> configureOptions)
            => builder.AddBasic(DevelopmentAuthenticationDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme, Action<DevelopmentAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<DevelopmentAuthenticationOptions, DevelopmentAuthenticationHandler>(authenticationScheme, configureOptions);
        }
    }

    public static class DevelopmentAuthenticationAppBuilderExtensions
    {
        public static AuthenticationBuilder AddDevelopment(this AuthenticationBuilder app,
            params DevelopmentUser[] users)
        {
            return app.AddBasic(
                config => {
                    config.Realm = DevelopmentAuthenticationDefaults.AuthenticationRealm;
                    config.Events = new DevelopmentAuthenticationEvents
                    {
                        OnValidateCredentials = context =>
                        {
                            foreach (var user in users)
                            {
                                if (context.Username == user.Username && context.Password == user.Password)
                                {
                                    var claims = new[]
                                    {
                                        new Claim(ClaimTypes.NameIdentifier, user.Subject, ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                        new Claim(ClaimTypes.Name, user.Username, ClaimValueTypes.String, context.Options.ClaimsIssuer)
                                    };

                                    var roleClaims = user.Roles.Select(x => new Claim(ClaimTypes.Role, x, ClaimValueTypes.String, context.Options.ClaimsIssuer));
                                    context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims.Union(roleClaims), context.Scheme.Name));
                                    context.Success();
                                }
                            }
                            return Task.CompletedTask;
                        }
                    };
                }
            );
        }
    }

    public class DevelopmentUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string[] Roles { get; set; }
    }
}
