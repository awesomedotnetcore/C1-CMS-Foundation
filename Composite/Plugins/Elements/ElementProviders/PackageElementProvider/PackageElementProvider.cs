﻿using System;
using System.Collections.Generic;
using System.Linq;
using Composite.Data;
using Composite.Data.Types;
using Composite.C1Console.Elements;
using Composite.C1Console.Elements.Plugins.ElementProvider;
using Composite.Core.PackageSystem;
using Composite.Core.ResourceSystem;
using Composite.Core.ResourceSystem.Icons;
using Composite.C1Console.Security;
using Composite.C1Console.Workflow;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;


namespace Composite.Plugins.Elements.ElementProviders.PackageElementProvider
{
    [ConfigurationElementType(typeof(AddOnElementProviderData))]
    internal sealed class PackageElementProvider : IHooklessElementProvider, IAuxiliarySecurityAncestorProvider
    {
        private ElementProviderContext _context;

        private static ResourceHandle RootClosedIcon = GetIconHandle("package-element-closed-root");
        private static ResourceHandle RootOpenedIcon = GetIconHandle("package-element-opened-root");
        private static ResourceHandle AvailablePackagesClosedIcon = GetIconHandle("package-element-closed-available");
        private static ResourceHandle AvailablePackagesOpenedIcon = GetIconHandle("package-element-opened-available");
        private static ResourceHandle InstalledPackagesClosedIcon = GetIconHandle("package-element-closed-installed");
        private static ResourceHandle InstalledPackagesOpenedIcon = GetIconHandle("package-element-opened-installed");
        private static ResourceHandle PackageSourcesClosedIcon = GetIconHandle("package-element-closed-sources");
        private static ResourceHandle PackageSourcesOpenedIcon = GetIconHandle("package-element-opened-sources");
        private static ResourceHandle PackageSourceItemClosedIcon = GetIconHandle("package-element-closed-sourceitem");
        private static ResourceHandle AvailablePackagesGroupClosedIcon = GetIconHandle("package-element-closed-availablegroup");
        private static ResourceHandle AvailablePackagesGroupOpenedIcon = GetIconHandle("package-element-opened-availablegroup");
        private static ResourceHandle LocalPackagesClosedIcon = GetIconHandle("package-element-closed-local");
        private static ResourceHandle LocalPackagesOpenedIcon = GetIconHandle("package-element-opened-local");
        private static ResourceHandle InstalledPackagesGroupClosedIcon = GetIconHandle("package-element-closed-installedgroup");
        private static ResourceHandle InstalledPackagesGroupOpenedIcon = GetIconHandle("package-element-opened-installedgroup");
        private static ResourceHandle AvailablePackageItemIcon = GetIconHandle("package-element-closed-availableitem");
        private static ResourceHandle AvailableCommercialPackageItemIcon = GetIconHandle("package-element-item-commercial");
        private static ResourceHandle InstalledPackageItemIcon = GetIconHandle("package-element-closed-installeditem");
        private static ResourceHandle InstalledCommercialPackageItemIcon = GetIconHandle("package-element-item-commercial");

        private static ResourceHandle ClearServerCacheIcon = GetIconHandle("package-clear-servercache");
        private static ResourceHandle ViewAvailableInformationIcon = GetIconHandle("package-view-availableinfo");
        private static ResourceHandle ViewInstalledInformationIcon = GetIconHandle("package-view-installedinfo");
        private static ResourceHandle InstallLocalPackageIcon = GetIconHandle("package-install-local-package");
        private static ResourceHandle AddPackageSourceIcon = GetIconHandle("package-add-source");
        private static ResourceHandle DeletePackageSourceIcon = GetIconHandle("package-delete-source");
        

        private static readonly ActionGroup PrimaryActionGroup = new ActionGroup(ActionGroupPriority.PrimaryHigh);


        public PackageElementProvider()
        {
            AuxiliarySecurityAncestorFacade.AddAuxiliaryAncestorProvider<DataEntityToken>(this);
        }


        public ElementProviderContext Context
        {
            set { _context = value; }
        }



        public IEnumerable<Element> GetRoots(SearchToken seachToken)
        {
            Element element = new Element(_context.CreateElementHandle(new PackageElementProviderRootEntityToken()));
            element.VisualData = new ElementVisualizedData
            {
                Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "RootFolderLabel"),
                ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "RootFolderToolTip"),
                HasChildren = true,
                Icon = RootClosedIcon,
                OpenedIcon = RootOpenedIcon
            };

            yield return element;
        }



        public IEnumerable<Element> GetChildren(EntityToken entityToken, SearchToken seachToken)
        {
            if ((entityToken is PackageElementProviderRootEntityToken) == true)
            {
                return GetRootChildren(seachToken);
            }
            else if ((entityToken is PackageElementProviderAvailablePackagesFolderEntityToken) == true)
            {
                return GetAvailableAddOnsFolderChildren(seachToken);
            }
            else if ((entityToken is PackageElementProviderAvailablePackagesGroupFolderEntityToken) == true)
            {
                PackageElementProviderAvailablePackagesGroupFolderEntityToken castedToken = entityToken as PackageElementProviderAvailablePackagesGroupFolderEntityToken;

                return GetAvailableAddOnGroupFolderChildren(castedToken.GroupName, seachToken);
            }
            else if ((entityToken is PackageElementProviderInstalledPackageFolderEntityToken) == true)
            {
                return GetInstalledAddOnFolderChildren(seachToken);
            }
            else if ((entityToken is PackageElementProviderPackageSourcesFolderEntityToken) == true)
            {
                return GetAddOnSourcesFolderChildren(seachToken);
            }
            else if ((entityToken is PackageElementProviderInstalledPackageLocalPackagesFolderEntityToken) == true)
            {
                return GetInstalledLocalAddOnsFolderChildren(seachToken);
            }
            else if ((entityToken is PackageElementProviderInstalledPackageGroupFolderEntityToken) == true)
            {
                PackageElementProviderInstalledPackageGroupFolderEntityToken castedToken = entityToken as PackageElementProviderInstalledPackageGroupFolderEntityToken;

                return GetInstalledAddOnGroupFolderChildren(castedToken.GroupName, seachToken);
            }

            throw new NotImplementedException();
        }



        public Dictionary<EntityToken, IEnumerable<EntityToken>> GetParents(IEnumerable<EntityToken> entityTokens)
        {
            Dictionary<EntityToken, IEnumerable<EntityToken>> result = new Dictionary<EntityToken, IEnumerable<EntityToken>>();

            foreach (EntityToken entityToken in entityTokens)
            {
                DataEntityToken dataEntityToken = entityToken as DataEntityToken;

                Type type = dataEntityToken.InterfaceType;
                if (type != typeof(IPackageServerSource)) continue;

                PackageElementProviderPackageSourcesFolderEntityToken newEntityToken = new PackageElementProviderPackageSourcesFolderEntityToken();

                result.Add(entityToken, new EntityToken[] { newEntityToken });
            }

            return result;
        }



        private IEnumerable<Element> GetRootChildren(SearchToken seachToken)
        {
            Element availableAddOnsElement = new Element(_context.CreateElementHandle(new PackageElementProviderAvailablePackagesFolderEntityToken()));
            availableAddOnsElement.VisualData = new ElementVisualizedData
            {
                Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "AvailableAddOnsFolderLabel"),
                ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "AvailableAddOnsFolderToolTip"),
                HasChildren = true,
                Icon = AvailablePackagesClosedIcon,
                OpenedIcon = AvailablePackagesOpenedIcon
            };
            availableAddOnsElement.AddAction(new ElementAction(new ActionHandle(new ClearServerCacheActionToken()))
            {
                VisualData = new ActionVisualizedData
                {
                    Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "ClearServerCacheLabel"),
                    ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "ClearServerCacheToolTip"),
                    Disabled = false,
                    Icon = ClearServerCacheIcon,
                    ActionLocation = new ActionLocation
                    {
                        ActionType = ActionType.Other,
                        IsInFolder = false,
                        IsInToolbar = false,
                        ActionGroup = PrimaryActionGroup
                    }
                }
            });            
            yield return availableAddOnsElement;



            Element installedAddOnsElement = new Element(_context.CreateElementHandle(new PackageElementProviderInstalledPackageFolderEntityToken()));
            installedAddOnsElement.VisualData = new ElementVisualizedData
            {
                Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "InstalledAddOnFolderLabel"),
                ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "InstalledAddOnFolderToolTip"),
                HasChildren = true,
                Icon = InstalledPackagesClosedIcon,
                OpenedIcon = InstalledPackagesOpenedIcon
            };
            yield return installedAddOnsElement;



            Element packageSourcesElement = new Element(_context.CreateElementHandle(new PackageElementProviderPackageSourcesFolderEntityToken()));
            packageSourcesElement.VisualData = new ElementVisualizedData
            {
                Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "AddOnSourcesFolderLabel"),
                ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "AddOnSourcesFolderToolTip"),
                HasChildren = DataFacade.GetData<IPackageServerSource>().Count() > 0,
                Icon = PackageSourcesClosedIcon,
                OpenedIcon = PackageSourcesOpenedIcon
            };
            packageSourcesElement.AddAction(new ElementAction(new ActionHandle(new WorkflowActionToken(WorkflowFacade.GetWorkflowType("Composite.Plugins.Elements.ElementProviders.PackageElementProvider.AddPackageSourceWorkflow"), new PermissionType[] { PermissionType.Administrate })))
            {
                VisualData = new ActionVisualizedData
                {
                    Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "AddAddOnSourceLabel"),
                    ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "AddAddOnSourceToolTip"),
                    Disabled = false,
                    Icon = AddPackageSourceIcon,
                    ActionLocation = new ActionLocation
                    {
                        ActionType = ActionType.Add,
                        IsInFolder = false,
                        IsInToolbar = true,
                        ActionGroup = PrimaryActionGroup
                    }
                }
            });
            yield return packageSourcesElement;
        }



        private IEnumerable<Element> GetAvailableAddOnsFolderChildren(SearchToken seachToken)
        {
            IEnumerable<string> groupNames =
                (from description in PackageSystemServices.GetFilteredAllAvailablePackages()
                 select description.GroupName).Distinct();

            foreach (string groupName in groupNames.OrderBy(f=>f))
            {
                Element element = new Element(_context.CreateElementHandle(new PackageElementProviderAvailablePackagesGroupFolderEntityToken(groupName)));
                element.VisualData = new ElementVisualizedData
                {
                    Label = groupName,
                    ToolTip = groupName,
                    HasChildren = true,
                    Icon = AvailablePackagesGroupClosedIcon,
                    OpenedIcon = AvailablePackagesGroupOpenedIcon
                };                


                yield return element;
            }
        }



        private IEnumerable<Element> GetAvailableAddOnGroupFolderChildren(string groupName, SearchToken seachToken)
        {
            IEnumerable<PackageDescription> packageDescriptions =
                (from description in PackageSystemServices.GetFilteredAllAvailablePackages()
                 where description.GroupName == groupName
                 orderby description.Name
                 select description);

            foreach (PackageDescription packageDescription in packageDescriptions)
            {
                ResourceHandle packageIcon = (packageDescription.PriceAmmount > 0 ? AvailableCommercialPackageItemIcon : AvailablePackageItemIcon);

                Element element = new Element(_context.CreateElementHandle(new PackageElementProviderAvailablePackagesItemEntityToken(
                    packageDescription.Id.ToString(),
                    packageDescription.GroupName)));
                element.VisualData = new ElementVisualizedData
                {
                    Label = packageDescription.Name,
                    ToolTip = packageDescription.Name,
                    HasChildren = false,
                    Icon = packageIcon,
                };

                element.AddAction(new ElementAction(new ActionHandle(new WorkflowActionToken(WorkflowFacade.GetWorkflowType("Composite.Plugins.Elements.ElementProviders.PackageElementProvider.ViewAvailablePackageInfoWorkflowWorkflow"), new PermissionType[] { PermissionType.Administrate })))
                {
                    VisualData = new ActionVisualizedData
                    {
                        Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "ViewAvailableInformationLabel"),
                        ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "ViewAvailableInformationToolTip"),
                        Icon = ViewInstalledInformationIcon,
                        Disabled = false,
                        ActionLocation = new ActionLocation
                        {
                            ActionType = ActionType.Edit,
                            IsInFolder = false,
                            IsInToolbar = true,
                            ActionGroup = PrimaryActionGroup
                        }

                    }
                });

                yield return element;
            }
        }



        private IEnumerable<Element> GetInstalledAddOnFolderChildren(SearchToken seachToken)
        {
            bool hasLocalAddonChildren =
                (from info in PackageManager.GetInstalledPackages()
                 where info.IsLocalInstalled == true
                 select info.Name).FirstOrDefault() != null;

            Element localAddOnsElement = new Element(_context.CreateElementHandle(new PackageElementProviderInstalledPackageLocalPackagesFolderEntityToken()));
            localAddOnsElement.VisualData = new ElementVisualizedData
            {
                Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "LocalAddOnsFolderLabel"),
                ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "LocalAddOnsFolderToolTip"),
                HasChildren = hasLocalAddonChildren,
                Icon = LocalPackagesClosedIcon,
                OpenedIcon = LocalPackagesOpenedIcon
            };

            localAddOnsElement.AddAction(new ElementAction(new ActionHandle(new WorkflowActionToken(WorkflowFacade.GetWorkflowType("Composite.Plugins.Elements.ElementProviders.PackageElementProvider.InstallLocalPackageWorkflow"), new PermissionType[] { PermissionType.Administrate })))
            {
                VisualData = new ActionVisualizedData
                {
                    Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "InstallLocalAddOnLabel"),
                    ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "InstallLocalAddOnToolTip"),
                    Disabled = false,
                    Icon = InstallLocalPackageIcon,
                    ActionLocation = new ActionLocation
                    {
                        ActionType = ActionType.Add,
                        IsInFolder = false,
                        IsInToolbar = true,
                        ActionGroup = PrimaryActionGroup
                    }
                }
            });


            yield return localAddOnsElement;


            IEnumerable<string> groupNames =
                (from info in PackageManager.GetInstalledPackages()
                 where info.IsLocalInstalled == false
                 orderby info.GroupName
                 select info.GroupName).Distinct();

            foreach (string groupName in groupNames)
            {
                Element element = new Element(_context.CreateElementHandle(new PackageElementProviderInstalledPackageGroupFolderEntityToken(groupName)));
                element.VisualData = new ElementVisualizedData
                {
                    Label = groupName,
                    ToolTip = groupName,
                    HasChildren = true,
                    Icon = InstalledPackagesGroupClosedIcon,
                    OpenedIcon = InstalledPackagesGroupOpenedIcon
                };

                yield return element;
            }
        }



        private IEnumerable<Element> GetAddOnSourcesFolderChildren(SearchToken seachToken)
        {
            List<IPackageServerSource> packageServerSources = 
                (from a in DataFacade.GetData<IPackageServerSource>()
                 orderby a.Url
                 select a).ToList();

            foreach (IPackageServerSource packageServerSource in packageServerSources)
            {
                Element element = new Element(_context.CreateElementHandle(packageServerSource.GetDataEntityToken()));
                element.VisualData = new ElementVisualizedData
                {
                    Label = packageServerSource.Url,
                    ToolTip = packageServerSource.Url,
                    HasChildren = false,
                    Icon = PackageSourceItemClosedIcon
                };

                element.AddAction(new ElementAction(new ActionHandle(new WorkflowActionToken(WorkflowFacade.GetWorkflowType("Composite.Plugins.Elements.ElementProviders.PackageElementProvider.DeletePackageSourceWorkflow"), new PermissionType[] { PermissionType.Administrate })))
                {
                    VisualData = new ActionVisualizedData
                    {
                        Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "DeleteAddOnSourceLabel"),
                        ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "DeleteAddOnSourceToolTip"),
                        Icon = DeletePackageSourceIcon,
                        Disabled = false,
                        ActionLocation = new ActionLocation
                        {
                            ActionType = ActionType.Delete,
                            IsInFolder = false,
                            IsInToolbar = true,
                            ActionGroup = PrimaryActionGroup
                        }

                    }
                });

                yield return element;
            }
        }



        private IEnumerable<Element> GetInstalledLocalAddOnsFolderChildren(SearchToken seachToken)
        {
            IEnumerable<InstalledPackageInformation> installedAddOnInformations =
                from info in PackageManager.GetInstalledPackages()
                where info.IsLocalInstalled == true
                orderby info.Name
                select info;

            foreach (InstalledPackageInformation installedAddOnInformation in installedAddOnInformations)
            {
                Element element = new Element(_context.CreateElementHandle(new PackageElementProviderInstalledPackageItemEntityToken(
                    installedAddOnInformation.Id,
                    installedAddOnInformation.GroupName,
                    installedAddOnInformation.IsLocalInstalled,
                    installedAddOnInformation.CanBeUninstalled)));

                element.VisualData = new ElementVisualizedData
                {
                    Label = installedAddOnInformation.Name,
                    ToolTip = installedAddOnInformation.Name,
                    HasChildren = false,
                    Icon = GetIconForPackageItem(installedAddOnInformation.Id),
                };

                element.AddAction(new ElementAction(new ActionHandle(new WorkflowActionToken(WorkflowFacade.GetWorkflowType("Composite.Plugins.Elements.ElementProviders.PackageElementProvider.ViewInstalledPackageInfoWorkflow"), new PermissionType[] { PermissionType.Administrate })))
                {
                    VisualData = new ActionVisualizedData
                    {
                        Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "ViewInstalledInformationLabel"),
                        ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "ViewInstalledInformationToolTip"),
                        Icon = ViewInstalledInformationIcon,
                        Disabled = false,
                        ActionLocation = new ActionLocation
                        {
                            ActionType = ActionType.Edit,
                            IsInFolder = false,
                            IsInToolbar = true,
                            ActionGroup = PrimaryActionGroup
                        }

                    }
                });

                yield return element;
            }
        }

        private ResourceHandle GetIconForPackageItem(Guid packageId)
        {

            ResourceHandle icon = InstalledPackageItemIcon;
            PackageLicenseDefinition licenseDef = PackageLicenseHelper.GetLicenseDefinition(packageId);
            if (licenseDef != null && !licenseDef.Permanent)
            {
                icon = InstalledCommercialPackageItemIcon;
            }
            return icon;
        }


        private IEnumerable<Element> GetInstalledAddOnGroupFolderChildren(string groupName, SearchToken seachToken)
        {
            IEnumerable<InstalledPackageInformation> installedAddOnInformations =
                from info in PackageManager.GetInstalledPackages()
                where info.GroupName == groupName &&
                      info.IsLocalInstalled == false
                orderby info.Name
                select info;

            foreach (InstalledPackageInformation installedAddOnInformation in installedAddOnInformations)
            {
                Element element = new Element(_context.CreateElementHandle(new PackageElementProviderInstalledPackageItemEntityToken(
                    installedAddOnInformation.Id,
                    installedAddOnInformation.GroupName,
                    installedAddOnInformation.IsLocalInstalled,
                    installedAddOnInformation.CanBeUninstalled)));

                element.VisualData = new ElementVisualizedData
                {
                    Label = installedAddOnInformation.Name,
                    ToolTip = installedAddOnInformation.Name,
                    HasChildren = false,
                    Icon = GetIconForPackageItem(installedAddOnInformation.Id),
                };

                element.AddAction(new ElementAction(new ActionHandle(new WorkflowActionToken(WorkflowFacade.GetWorkflowType("Composite.Plugins.Elements.ElementProviders.PackageElementProvider.ViewInstalledPackageInfoWorkflow"), new PermissionType[] { PermissionType.Administrate })))
                {
                    VisualData = new ActionVisualizedData
                    {
                        Label = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "ViewInstalledInformationLabel"),
                        ToolTip = StringResourceSystemFacade.GetString("Composite.Plugins.PackageElementProvider", "ViewInstalledInformationToolTip"),
                        Icon = ViewInstalledInformationIcon,
                        Disabled = false,
                        ActionLocation = new ActionLocation
                        {
                            ActionType = ActionType.Edit,
                            IsInFolder = false,
                            IsInToolbar = true,
                            ActionGroup = PrimaryActionGroup
                        }

                    }
                });

                yield return element;
            }
        }




        private static ResourceHandle GetIconHandle(string name)
        {
            return new ResourceHandle(BuildInIconProviderName.ProviderName, name);
        }        
    }


    [Assembler(typeof(NonConfigurableHooklessElementProviderAssembler))]
    internal sealed class AddOnElementProviderData : HooklessElementProviderData
    {
    }
}
