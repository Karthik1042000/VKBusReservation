using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel;
//using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;

namespace VKBusReservation.Models
{

    public static class Common
    {
        public static string GetClaimRole(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim role = claimsIdentity.FindFirst(ClaimTypes.Role);
                return role.Value;
        }
        public static string GetClaimName(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim name = claimsIdentity.FindFirst(ClaimTypes.Name);
            return name.Value;
        }
        public static string GetClaimId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim id = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return id.Value;
        }
        public static string GetClaimEmail(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim email = claimsIdentity.FindFirst(ClaimTypes.Email);
            return email.Value;  
        }
    }
}
