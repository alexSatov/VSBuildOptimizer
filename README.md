# VSBuildOptimizer

<!-- Update the VS Gallery link after you upload the VSIX-->
Download this extension from the [VS Gallery](https://visualstudiogallery.msdn.microsoft.com/[GuidFromGallery])
or get the [CI build](http://vsixgallery.com/extension/5a6a87d6-88ba-43fb-b257-df2d514af96b/).

---------------------------------------

Project for optimizing build speed by reducing unnecessary project\lib copying. 
Project also can show independent projects in your solution and check all lib reference on the same versions and .dll paths.

See the [change log](CHANGELOG.md) for changes and road map.

## Features

- Show independent projects in solution
- Check inner project references on same versions and .dll paths
- Optimize all references for selected project and build _all_ solution

### Independent Projects
Select solution in solution explorer and right-click.

![Sol Ctxt Menu](images/sol_ctxt_menu.png)

### Reference checking
Select project in solution explorer and right-click.

![Proj Ctxt Menu1](images/proj_ctxt_menu1.png)

### Optimized build
Select project in solution explorer and right-click.

![Proj Ctxt Menu2](images/proj_ctxt_menu2.png)

## Contribute
Check out the [contribution guidelines](CONTRIBUTING.md)
if you want to contribute to this project.

For cloning and building this project yourself, make sure
to install the
[Extensibility Tools 2015](https://visualstudiogallery.msdn.microsoft.com/ab39a092-1343-46e2-b0f1-6a3f91155aa6)
extension for Visual Studio which enables some features
used by this project.

## License
[Apache 2.0](LICENSE)