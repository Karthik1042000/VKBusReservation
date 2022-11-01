//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using System;
//using System.ComponentModel;
////using Castle.Components.DictionaryAdapter;
//using System.ComponentModel.DataAnnotations;
//using System.Security.Claims;
//using System.Security.Principal;

//namespace VKBusReservation.Models
//{

//    public static class Common
//    {
//    public static string GetClaimValue(this IPrincipal currentPrincipal, string Role)
//    {
//            var identity = currentPrincipal.Identity as ClaimsIdentity;
//            if (identity == null)
//                return null;

//            var claim = identity.Claims.FirstOrDefault(c => c.Type == Role);
//            return claim?.Value;
//        }
//    public string GetRole()
//    {
//        string role = string.Empty;
//        if ()
//        {
//            var claim = (ClaimsIdentity)this.RequestContext.Principal.Identity;
//            role = claim.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
//        }
//        return role;
//    }
//    public static bool IsAdmin()
//    {
//        if (GetRole() == "Admin")
//        {
//            return true;
//        }
//        return false;
//    }
//    }
//}
