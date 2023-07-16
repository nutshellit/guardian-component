# guardian-component
Example of a Razor Class Library component hosted in multiple environments

## Guardian component
The shared component is a simple self-contained razor class library (RCL) that performs a simple search against the [Guardian Api](https://open-platform.theguardian.com/documentation/) and displays the results in a paged list. You will need to sign-up for a key although 'test' seem to work for low volume queries.

## Maui hosting
![Maui hosting](/readme_images/maui_hosted.png) 
You just need to reference the RCL and can add the component like a normal blazor component.
![Maui bootstrap](/readme_images/maui_bootstrap.png)

## Blazor hosting
![Blazor hosting](/readme_images/blazor_hosted.png)
You just need to reference the RCL and add the component like a normal Blazor component. There is an extension method 'AddGuardianServices' in the component that needs to be added and also an appsettings.json has been added with an 'IsBlazorHosted' to disable default bootstrapping when hosting in aspnet .
![Blazor bootstrap](/readme_images/blazor_bootstrap.png)

## Aspnet core hosting
![Aspnet hosting](/readme_images/asp_net_hosted.png)
There are a few steps to follow. This is a good guide [Blazor Hosting](https://www.telerik.com/blogs/integrate-blazor-webassembly-existing-aspnet-core-web-application).

1. Create a simple wrapper component in the blazor application to wrap the RCL. I have called it GuardianShared. 
2. Reference Nuget package Microsoft.AspNetCore.Components.WebAssembly.Server
3. Add   <script src="_framework/blazor.webassembly.js"></script> to Layout page or page that is hosting the Blazor component.
4. Make sure that  builder.RootComponents.Add<App>("#app") is disabled in the blazor application.
5. Update program.cs with following changes in green ![Aspnet Bootstrap](/readme_images/aspnet_bootstrap.png)
6. The component itself is rendered as follows in the page 
![Aspnet component](/readme_images/aspnet_component.png)


