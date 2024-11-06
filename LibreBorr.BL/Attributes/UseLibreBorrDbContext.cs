using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using LibreBorr.Services.Models;


/// <summary>
/// This code creates a so-called descriptor-attribute and allows us to wrap GraphQL
/// configuration code into attributes that you can apply to .NET type system members.
/// </summary>
public class UseLibreBorrDbContextAttribute : ObjectFieldDescriptorAttribute
{
    protected override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
    {
        descriptor.Use<LibreBorrDbContext>();
    }
}