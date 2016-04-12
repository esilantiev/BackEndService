using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Ises.Domain.Actions;
using Ises.Domain.Areas;
using Ises.Domain.AreaTypes;
using Ises.Domain.HazardControls;
using Ises.Domain.HazardGroups;
using Ises.Domain.HazardRules;
using Ises.Domain.Hazards;
using Ises.Domain.Installations;
using Ises.Domain.IsolationMethods;
using Ises.Domain.IsolationStates;
using Ises.Domain.IsolationTypes;
using Ises.Domain.LeadDisciplines;
using Ises.Domain.Locations;
using Ises.Domain.Organizations;
using Ises.Domain.Positions;
using Ises.Domain.RolePermissions;
using Ises.Domain.UserRoles;
using Ises.Domain.WorkCertificateCategories;

namespace Ises.Data.Migrations
{
    public class DatabaseLoader
    {
        public static void Seed(DbContext context)
        {
            if (!context.Set<WorkCertificateCategory>().Any())
            {
                foreach (var workCertificateCategory in GetWorkCertificateCategories())
                {
                    context.Set<WorkCertificateCategory>().Add(workCertificateCategory);
                }
                context.SaveChanges();
            }

            if (!context.Set<AreaType>().Any())
            {
                foreach (var areaType in GetAreaTypes())
                {
                    context.Set<AreaType>().Add(areaType);
                }
                context.SaveChanges();
            }

            if (!context.Set<HazardGroup>().Any())
            {
                foreach (var hazardGroup in GetHazardGroups())
                {
                    context.Set<HazardGroup>().Add(hazardGroup);
                }
                context.SaveChanges();
            }

            if (!context.Set<Organization>().Any())
            {
                foreach (var organization in GetOrganizations())
                {
                    context.Set<Organization>().Add(organization);
                }
                context.SaveChanges();
            }

            if (!context.Set<Installation>().Any())
            {
                foreach (var installation in GetInstallations())
                {
                    context.Set<Installation>().Add(installation);
                }
                context.SaveChanges();
            }


            if (!context.Set<UserRole>().Any())
            {
                foreach (var userRole in GetUserRoles())
                {
                    context.Set<UserRole>().Add(userRole);
                }
                context.SaveChanges();
            }

            if (!context.Set<RolePermission>().Any())
            {
                foreach (var rolePermission in GetRolePermissions())
                {
                    context.Set<RolePermission>().Add(rolePermission);
                }
                context.SaveChanges();
            }

            if (!context.Set<Action>().Any())
            {
                foreach (var action in GetActions())
                {
                    context.Set<Action>().Add(action);
                }
                context.SaveChanges();
            }

            if (!context.Set<HazardControl>().Any())
            {
                foreach (var hazardControl in GetHazardControls())
                {
                    context.Set<HazardControl>().Add(hazardControl);
                }
                context.SaveChanges();
            }

            if (!context.Set<IsolationType>().Any())
            {
                foreach (var isolationType in GetIsolationTypes())
                {
                    context.Set<IsolationType>().Add(isolationType);
                }
                context.SaveChanges();
            }

            if (!context.Set<IsolationMethod>().Any())
            {
                foreach (var isolationMethod in GetIsolationMethods())
                {
                    context.Set<IsolationMethod>().Add(isolationMethod);
                }
                context.SaveChanges();
            }

            if (!context.Set<IsolationState>().Any())
            {
                foreach (var isolationState in GetIsolationStates())
                {
                    context.Set<IsolationState>().Add(isolationState);
                }
                context.SaveChanges();
                foreach (var isolationState in UpdateOppositeStates())
                {
                    context.Set<IsolationState>().AddOrUpdate(state=> state.Id, isolationState);
                }
                context.SaveChanges();
            }
        }

        private static IEnumerable<AreaType> GetAreaTypes()
        {
            var areaTypes = new List<AreaType>
            {
                new AreaType {Id = 1, Name = "Hazardous Area"},
                new AreaType {Id = 2, Name = "Non Hazardous Area"}
            };
            return areaTypes;
        }

        private static IEnumerable<Installation> GetInstallations()
        {
            var installations = new List<Installation>
            {
                new Installation {Name = "PA-A", Locations = new Collection<Location> 
                {
                    new Location {Name = "PA-A Upper Deck", Areas = new Collection<Area>
                    {
                        new Area { AreaTypeId = 1, Name = "Area 1H-"},
                        new Area { AreaTypeId = 2, Name = "Area 2NH"}
                    }},
                    new Location {Name = "PA-A Mezz Deck", Areas = new Collection<Area>
                    {
                        new Area { AreaTypeId = 1, Name = "Area 3H-"},
                        new Area { AreaTypeId = 2, Name = "Area 3NH"}
                    }},
                }},
                new Installation {Name = "PA-B", Locations = new Collection<Location> 
                {
                    new Location {Name = "PA-B Upper Deck", Areas = new Collection<Area>
                    {
                        new Area { AreaTypeId = 1, Name = "Area 4H-"},
                        new Area { AreaTypeId = 2, Name = "Area 4NH"}
                    }},
                    new Location {Name = "PA-B Mezz Deck", Areas = new Collection<Area>
                    {
                        new Area { AreaTypeId = 1, Name = "Area 5H-"},
                        new Area { AreaTypeId = 2, Name = "Area 5NH"}
                    }},
                }},
                new Installation {Name = "PA-C", Locations = new Collection<Location> 
                {
                    new Location {Name = "PA-C Upper Deck"},
                    new Location {Name = "PA-C Mezz Deck"}
                }},
                new Installation {Name = "PA-D3", Locations = new Collection<Location> 
                {
                    new Location {Name = "PA-D3 Upper Deck"},
                    new Location {Name = "PA-D3 Mezz Deck"}
                }},
                new Installation {Name = "PA-D4", Locations = new Collection<Location> 
                {
                    new Location {Name = "PA-D4 Upper Deck"},
                    new Location {Name = "PA-D4 Mezz Deck"}
                }}
            };
            return installations;
        }

        private static IEnumerable<WorkCertificateCategory> GetWorkCertificateCategories()
        {
            var workCertificateCategories = new List<WorkCertificateCategory>
            {
                new WorkCertificateCategory { Id = 1, Name = "Cold Work"},
                new WorkCertificateCategory { Id = 2, Name = "Hot Work (Cat 1)"},
                new WorkCertificateCategory { Id = 3, Name = "Hot Work (Cat 2)"},
                new WorkCertificateCategory { Id = 4, Name = "Breaking Containment"}
            };
            return workCertificateCategories;
        }

        private static IEnumerable<HazardGroup> GetHazardGroups()
        {
            var hazardGroups = new List<HazardGroup>
            {
                new HazardGroup { Name = "Category of worksite presence", Hazards = new Collection<Hazard>
                {
                    new Hazard { Id = 1, Name = "Cat A Performing Authority presence"},
                    new Hazard { Id = 2, Name = "Cat B Performing Authority presence"},
                    new Hazard { Id = 3, Name = "Cat C Performing Authority presence"}
                }},
                new HazardGroup { Name = "Confined Space Entry", Hazards = new Collection<Hazard>
                {
                    new Hazard { Id = 4, Name = "Leg Entry"},
                    new Hazard { Id = 5, Name = "Life threatening atmosphere (Confined Space)"}
                }},
                new HazardGroup { Name = "Diving and Well Operations", Hazards = new Collection<Hazard>
                {
                    new Hazard { Id = 7, Name = "Diving Operations"},
                    new Hazard { Id = 8, Name = "Well Operations"}
                }},
                new HazardGroup {Name = "Electrical Energy", Hazards = new Collection<Hazard>
                {
                    new Hazard { Id = 9, Name = "Electromagnetic Radiation (Radhaz)"},
                    new Hazard { Id = 10, Name = "Extra Low Voltage (ELV)"},
                    new Hazard { Id = 11, Name = "High voltage (HV)"},
                    new Hazard { Id = 12, Name = "Low voltage (LV)"},
                    new Hazard { Id = 13, Name = "Stored electrical charge"}
                }},
                new HazardGroup { Name = "Equipment Hazards", Hazards = new Collection<Hazard>
                {                  
                    new Hazard {Id = 14, Name = "Failure of small bore pipework"},
                    new Hazard {Id = 15, Name = "Lifting Equipment Failure"},
                    new Hazard {Id = 16, Name = "Low Power Lasers"},
                    new Hazard {Id = 17, Name = "Movement of HGVs within assets fences"},
                    new Hazard {Id = 18, Name = "Pressure testing"},
                    new Hazard {Id = 19, Name = "Pressurised gas cylinder failure"},
                    new Hazard {Id = 20, Name = "Pressurised hose failure"},
                    new Hazard {Id = 21, Name = "Pressurised vessel or system failure"},
                    new Hazard {Id = 22, Name = "Rotating machinery"},
                    new Hazard {Id = 23, Name = "Static discharge to electronic equipment"},
                    new Hazard {Id = 24, Name = "Stored mechanical energy"},
                    new Hazard {Id = 25, Name = "Transport vehicles"}
                }},
                new HazardGroup {Name = "Excavation"},
                new HazardGroup {Name = "GENERAL WORKSITE PREPARATIONS"},
                new HazardGroup {Name = "General Worksite Preparations"},
                new HazardGroup {Name = "Inadequate Control of Work"},
                new HazardGroup {Name = "Level 2 Risk Assessment not conducted adequately"},
                new HazardGroup {Name = "Materials & substances"},
                new HazardGroup {Name = "Safety System Impairment"},
                new HazardGroup {Name = "Working Environment"},
                new HazardGroup {Name = "Worksite Clean Up"}
            };
            return hazardGroups;
        }

        private static IEnumerable<Organization> GetOrganizations()
        {
            var organizations = new List<Organization>
            {
                new Organization {Name = "Organ1", LeadDisciplines = new List<LeadDiscipline>
                    {
                        new LeadDiscipline {Name = "Lead Dis1", Positions = new Collection<Position>
                        {
                            new Position {Name = "Position1"},
                            new Position {Name = "Position2"}
                        }},
                        new LeadDiscipline {Name = "Lead Dis2", Positions = new Collection<Position>
                        {
                            new Position {Name = "Position3"},
                            new Position {Name = "Position4"}
                        }},
                    }},
                new Organization {Name = "Organ2", LeadDisciplines = new List<LeadDiscipline>
                    {
                        new LeadDiscipline {Name = "Lead Dis3"},
                        new LeadDiscipline {Name = "Lead Dis4"}
                    }},
                new Organization {Name = "Organ3", LeadDisciplines = new List<LeadDiscipline>
                    {
                        new LeadDiscipline {Name = "Lead Dis5"},
                        new LeadDiscipline {Name = "Lead Dis6"}
                    }},
            };
            return organizations;
        }

        private static IEnumerable<UserRole> GetUserRoles()
        {
            var userRoles = new List<UserRole>
            {
                new UserRole {Name = "Руководитель Участка", ShortEnglishName = "AA"},
                new UserRole {Name = "Уполномоченный специалист по анализу газа", ShortEnglishName = "AGT"},
                new UserRole {Name = "Оператор диспетчерской", ShortEnglishName = "CRO"},
                new UserRole {Name = "Уполномоченный по изоляции", ShortEnglishName = "IA"},
                new UserRole {Name = "R Assessor", ShortEnglishName = "RA"}
            };
            return userRoles;
        }

        private static IEnumerable<RolePermission> GetRolePermissions()
        {
            var rolePermissions = new List<RolePermission>
            {
                new RolePermission {Id = 1, Name = "Просмотр всех сертификатов и страниц пользователей"},
                new RolePermission {Id = 2, Name = "Создание страницы пользователя"},
                new RolePermission {Id = 3, Name = "Авторизация пользователя"},
                new RolePermission {Id = 4, Name = "Создание WCC"},
                new RolePermission {Id = 5, Name = "Редактирование WCC"},
                new RolePermission {Id = 6, Name = "Создание ICC"},
                new RolePermission {Id = 7, Name = "Редактирование ICC"},
                new RolePermission {Id = 8, Name = "Обновление точек ICC"},
                new RolePermission {Id = 9, Name = "Изменение статуса WCC c Under review на Reviewed"},
                new RolePermission {Id = 10, Name = "Изменение статуса WCC c Reviewed на Authorized"},
                new RolePermission {Id = 11, Name = "Изменение статуса WCC c Authorized на Ready"},
                new RolePermission {Id = 12, Name = "Изменение статуса WCC c Ready на Live"},
                new RolePermission {Id = 13, Name = "Изменение статуса WCC с Live на Job completed"},
                new RolePermission {Id = 14, Name = "Изменение статуса WCC с Live на Suspend"},
                new RolePermission {Id = 15, Name = "Изменение статуса WCC с Suspend на Ready"},
                new RolePermission {Id = 16, Name = "Изменение статуса WCC с Job completed на Archived"},
                new RolePermission {Id = 17, Name = "Изменение статуса ICC c Requested на Correct"},
                new RolePermission {Id = 18, Name = "Изменение статуса ICC c In progress на In place"},
                new RolePermission {Id = 19, Name = "Изменение статуса ICC c In place на Deisolation in progress"},
                new RolePermission {Id = 20, Name = "Изменение статуса ICC c Deisolation in progress на Deisolated"},
                new RolePermission {Id = 21, Name = "Изменение статуса ICC c Deisolated на Archived"},
                new RolePermission {Id = 22, Name = "Печать WCC и ICC"},
                new RolePermission {Id = 23, Name = "Возможность быть аксессором"},
                new RolePermission {Id = 24, Name = "Возможность быть прикрепленым к эрии"},
                new RolePermission {Id = 25, Name = "Auditor"},
                new RolePermission {Id = 26, Name = "Level 2 Risk Assessor"},
                new RolePermission {Id = 27, Name = "Translator"}
            };
            return rolePermissions;
        }

        private static IEnumerable<Action> GetActions()
        {
            var actions = new List<Action>
            {
                new Action {Name = "WCC verification is required"},
                new Action {Name = "Your signature is required"},
                new Action {Name = "RAL2 signature is required"},
                new Action {Name = "Isolation status confirmation"},
                new Action {Name = "ICC verification is required"},
                new Action {Name = "Your signature is required"},
                new Action {Name = "RAL2 signature is required"},
                new Action {Name = "Isolation status confirmation"},
            };
            return actions;
        }

        private static IEnumerable<HazardRule> GetHazardRules()
        {
            var hazardRules = new List<HazardRule>
            {
                new HazardRule { Id = 1,  AreaTypeId = 1, WorkCertificateCategoryId = 1},
                new HazardRule { Id = 2,  AreaTypeId = 1, WorkCertificateCategoryId = 2},
                new HazardRule { Id = 3,  AreaTypeId = 1, WorkCertificateCategoryId = 3},
                new HazardRule { Id = 4,  AreaTypeId = 1, WorkCertificateCategoryId = 4},
                new HazardRule { Id = 5,  AreaTypeId = 2, WorkCertificateCategoryId = 1},
                new HazardRule { Id = 6,  AreaTypeId = 2, WorkCertificateCategoryId = 2},
                new HazardRule { Id = 7,  AreaTypeId = 2, WorkCertificateCategoryId = 3},
                new HazardRule { Id = 8,  AreaTypeId = 2, WorkCertificateCategoryId = 4}
            };
            return hazardRules;
        }

        private static IEnumerable<HazardControl> GetHazardControls()
        {
            var firstHazardRule = GetHazardRules().First();

            var hazardControls = new List<HazardControl>
            {
                new HazardControl { Id = 1, HazardId = 1, Name = "Continuous attendance of a Performing authority. Work to stop when no PA present.", HazardRules = new Collection<HazardRule>
                    {
                        firstHazardRule
                    }},
                new HazardControl { Id = 2, HazardId = 1, Name = "Red barriers and signs are to be erected. Barriers to be removed as soon as possible to do so.", HazardRules = new Collection<HazardRule>
                    {
                        firstHazardRule
                    }},
                new HazardControl { Id = 3, HazardId = 1, Name = "Yellow barriers and signs are to be erected. Barriers to be removed as soon as possible to do so."},
                new HazardControl { Id = 4, HazardId = 1, Name = "Hearing protection is provided. Earpluggs and earmuffs (disposable and custom moulded) are used. First action level 80 dB(A), second action level 85dB"},
                new HazardControl { Id = 5, HazardId = 1, Name = "Area delegate must sign paper copy of permit before work starts or countersign its electronically."},
                new HazardControl { Id = 6, HazardId = 1, Name = "GAS TEST REQUIRED BEFORE WORK COMMENCES"},
                new HazardControl { Id = 7, HazardId = 1, Name = "CONSTANT GAS MONITORING WITH PORTABLE DETECTOR"},
                new HazardControl { Id = 8, HazardId = 2, Name = "PA cannot be responsible for more than one CAT B unless the other tasks are in close proximity to the first one and PA can safely supervise all"},
                new HazardControl { Id = 9, HazardId = 2, Name = "Work may continue during short absences of the PA. PA cannot be responsible for more than 1 CAT B task."},
                new HazardControl { Id = 10, HazardId = 2, Name = "PA to ensure that the No. of Cat C permits accepted, in close proximity to CAT B, (if also responsible for Cat B permit), does not compromise safety."},
                new HazardControl { Id = 11, HazardId = 2, Name = "Red barriers and signs are to be erected. Barriers to be removed as soon as possible to do so."},
                new HazardControl { Id = 12, HazardId = 2, Name = "Yellow barriers and signs are to be erected. Barriers to be removed as soon as possible to do so."},
                new HazardControl { Id = 13, HazardId = 2, Name = "Hearing protection is provided. Earpluggs and earmuffs (disposable and custom moulded) are used. First action level 80 dB(A), second action level 85dB"},
                new HazardControl { Id = 14, HazardId = 2, Name = "Area delegate must sign paper copy of permit before work starts or countersign its electronically."},
                new HazardControl { Id = 15, HazardId = 2, Name = "GAS TEST REQUIRED BEFORE WORK COMMENCES"},
                new HazardControl { Id = 16, HazardId = 2, Name = "CONSTANT GAS MONITORING WITH PORTABLE DETECTOR"}
            };
            return hazardControls;
        }

        private static IEnumerable<IsolationType> GetIsolationTypes()
        {
            var isolationTypes = new List<IsolationType>
            {
                new IsolationType { Id = 1, Name = "Process"},
                new IsolationType { Id = 2, Name = "Mechanical"},
                new IsolationType { Id = 3, Name = "Electrical"},
                new IsolationType { Id = 4, Name = "Control"},
                new IsolationType { Id = 5, Name = "Safety"}
            };
            return isolationTypes;
        }

        private static IEnumerable<IsolationMethod> GetIsolationMethods()
        {
            var isolationMethods = new List<IsolationMethod>
            {
                new IsolationMethod {IsolationTypeId =1, Id = 1, Name = "Valve"},
                new IsolationMethod {IsolationTypeId =1, Id = 2, Name = "LcValve"},
                new IsolationMethod {IsolationTypeId =1, Id = 3, Name = "Blank"},
                new IsolationMethod {IsolationTypeId =1, Id = 4, Name = "Spade"},
                new IsolationMethod {IsolationTypeId =1, Id = 5, Name = "Disconnect"},
                new IsolationMethod {IsolationTypeId =1, Id = 6, Name = "ProcessBleed"},
                new IsolationMethod {IsolationTypeId =1, Id = 7, Name = "L/O Valve"},

                new IsolationMethod {IsolationTypeId =2, Id = 8, Name = "Valve"},
                new IsolationMethod {IsolationTypeId =2, Id = 9, Name = "Blank"},
                new IsolationMethod {IsolationTypeId =2, Id = 10, Name = "Spade / Spectacle"},
                new IsolationMethod {IsolationTypeId =2, Id = 11, Name = "Disconnect"},
                new IsolationMethod {IsolationTypeId =2, Id = 12, Name = "ProcessBleed"},

                new IsolationMethod {IsolationTypeId =3, Id = 13, Name = "Disconnect"},
                new IsolationMethod {IsolationTypeId =3, Id = 14, Name = "Earthed"},
                new IsolationMethod {IsolationTypeId =3, Id = 15, Name = "Fuse Removed"},
                new IsolationMethod {IsolationTypeId =3, Id = 16, Name = "Isolator Opened"},
                new IsolationMethod {IsolationTypeId =3, Id = 17, Name = "BreakerRemoved"},

                new IsolationMethod {IsolationTypeId =4, Id = 18, Name = "Disconnect"},
                new IsolationMethod {IsolationTypeId =4, Id = 19, Name = "Valve"},
                new IsolationMethod {IsolationTypeId =4, Id = 20, Name = "Fuse Removed"},
                new IsolationMethod {IsolationTypeId =4, Id = 21, Name = "Isolator Opened"},
                
                new IsolationMethod {IsolationTypeId =5, Id = 22, Name = "Inhibited"},
                new IsolationMethod {IsolationTypeId =5, Id = 23, Name = "Link Removed"},
                new IsolationMethod {IsolationTypeId =5, Id = 24, Name = "Over-ride"},
                new IsolationMethod {IsolationTypeId =5, Id = 25, Name = "Off Scan"},
                new IsolationMethod {IsolationTypeId =5, Id = 26, Name = "Valve"}
            };
            return isolationMethods;
        }

        private static IEnumerable<IsolationState> GetIsolationStates()
        {
            var isolationStates = new List<IsolationState>
            {
                new IsolationState {IsolationMethodId = 1, Id = 1, Name = "Open"},
                new IsolationState {IsolationMethodId = 1, Id = 2, Name = "Closed"},
                new IsolationState {IsolationMethodId = 2, Id = 3, Name = "Closed"},
                new IsolationState {IsolationMethodId = 2, Id = 4, Name = "Locked Closed"},
                new IsolationState {IsolationMethodId = 3, Id = 5, Name = "InPlace"},
                new IsolationState {IsolationMethodId = 3, Id = 6, Name = "Removed"},
                new IsolationState {IsolationMethodId = 4, Id = 7, Name = "Inserted"},
                new IsolationState {IsolationMethodId = 4, Id = 8, Name = "Removed"},
                new IsolationState {IsolationMethodId = 5, Id = 9, Name = "Disconnected"},
                new IsolationState {IsolationMethodId = 5, Id = 10, Name = "Reconnected"},
                new IsolationState {IsolationMethodId = 6, Id = 11, Name = "Labeled"},
                new IsolationState {IsolationMethodId = 6, Id = 12, Name = "Label Removed"},
                new IsolationState {IsolationMethodId = 7, Id = 13, Name = "Open"},
                new IsolationState {IsolationMethodId = 7, Id = 14, Name = "Locked Closed"},

                new IsolationState {IsolationMethodId = 8,  Id = 15, Name = "Open"},
                new IsolationState {IsolationMethodId = 8,  Id = 16, Name = "Closed"},
                new IsolationState {IsolationMethodId = 9,  Id = 17, Name = "In Place"},
                new IsolationState {IsolationMethodId = 9,  Id = 18, Name = "Removed"},
                new IsolationState {IsolationMethodId = 10, Id = 19, Name = "Inserted"},
                new IsolationState {IsolationMethodId = 10, Id = 20, Name = "Removed"},
                new IsolationState {IsolationMethodId = 11, Id = 21, Name = "Disconnected"},
                new IsolationState {IsolationMethodId = 11, Id = 22, Name = "Reconnected"},
                new IsolationState {IsolationMethodId = 12, Id = 23, Name = "Labeled"},
                new IsolationState {IsolationMethodId = 12, Id = 24, Name = "Label Removed"},

                new IsolationState {IsolationMethodId = 13, Id = 25, Name = "Disconnected"},
                new IsolationState {IsolationMethodId = 13, Id = 26, Name = "Reconnected"},
                new IsolationState {IsolationMethodId = 14, Id = 27, Name = "Applied"},
                new IsolationState {IsolationMethodId = 14, Id = 28, Name = "Removed"},
                new IsolationState {IsolationMethodId = 15, Id = 29, Name = "Removed"},
                new IsolationState {IsolationMethodId = 15, Id = 30, Name = "Replaced"},
                new IsolationState {IsolationMethodId = 16, Id = 31, Name = "Open"},
                new IsolationState {IsolationMethodId = 16, Id = 32, Name = "Closed"},
                new IsolationState {IsolationMethodId = 17, Id = 33, Name = "Removed"},
                new IsolationState {IsolationMethodId = 17, Id = 34, Name = "Replaced"},

                new IsolationState {IsolationMethodId = 18, Id = 35, Name = "Disconnected"},
                new IsolationState {IsolationMethodId = 18, Id = 36, Name = "Reconnected"},
                new IsolationState {IsolationMethodId = 19, Id = 37, Name = "Open"},
                new IsolationState {IsolationMethodId = 19, Id = 38, Name = "Closed"},
                new IsolationState {IsolationMethodId = 20, Id = 39, Name = "Removed"},
                new IsolationState {IsolationMethodId = 20, Id = 40, Name = "Replaced"},
                new IsolationState {IsolationMethodId = 21, Id = 41, Name = "Open"},
                new IsolationState {IsolationMethodId = 21, Id = 42, Name = "Closed"},

                new IsolationState {IsolationMethodId = 22, Id = 43, Name = "Inhibited"},
                new IsolationState {IsolationMethodId = 22, Id = 44, Name = "Reinstated"},
                new IsolationState {IsolationMethodId = 23, Id = 45, Name = "Removed"},
                new IsolationState {IsolationMethodId = 23, Id = 46, Name = "Replaced"},
                new IsolationState {IsolationMethodId = 24, Id = 47, Name = "On"},
                new IsolationState {IsolationMethodId = 24, Id = 48, Name = "Off"},
                new IsolationState {IsolationMethodId = 25, Id = 49, Name = "OffScan"},
                new IsolationState {IsolationMethodId = 25, Id = 50, Name = "OnScan"},
                new IsolationState {IsolationMethodId = 26, Id = 51, Name = "Open"},
                new IsolationState {IsolationMethodId = 26, Id = 52, Name = "Closed"},
            };
            return isolationStates;
        }

        private static IEnumerable<IsolationState> UpdateOppositeStates()
        {
            var isolationStates = new List<IsolationState>
            {
                new IsolationState {IsolationMethodId = 1, IsolationStateId = 2, Id = 1, Name = "Open"},
                new IsolationState {IsolationMethodId = 1, IsolationStateId = 1, Id = 2, Name = "Closed"},
                new IsolationState {IsolationMethodId = 2, IsolationStateId = 4, Id = 3, Name = "Closed"},
                new IsolationState {IsolationMethodId = 2, IsolationStateId = 3, Id = 4, Name = "Locked Closed"},
                new IsolationState {IsolationMethodId = 3, IsolationStateId = 6, Id = 5, Name = "InPlace"},
                new IsolationState {IsolationMethodId = 3, IsolationStateId = 5, Id = 6, Name = "Removed"},
                new IsolationState {IsolationMethodId = 4, IsolationStateId = 8, Id = 7, Name = "Inserted"},
                new IsolationState {IsolationMethodId = 4, IsolationStateId = 7, Id = 8, Name = "Removed"},
                new IsolationState {IsolationMethodId = 5, IsolationStateId = 10, Id = 9, Name = "Disconnected"},
                new IsolationState {IsolationMethodId = 5, IsolationStateId = 9, Id = 10, Name = "Reconnected"},
                new IsolationState {IsolationMethodId = 6, IsolationStateId = 12, Id = 11, Name = "Labeled"},
                new IsolationState {IsolationMethodId = 6, IsolationStateId = 11, Id = 12, Name = "Label Removed"},
                new IsolationState {IsolationMethodId = 7, IsolationStateId = 14, Id = 13, Name = "Open"},
                new IsolationState {IsolationMethodId = 7, IsolationStateId = 13, Id = 14, Name = "Locked Closed"},

                new IsolationState {IsolationMethodId = 8, IsolationStateId = 16, Id = 15, Name = "Open"},
                new IsolationState {IsolationMethodId = 8, IsolationStateId = 15, Id = 16, Name = "Closed"},
                new IsolationState {IsolationMethodId = 9, IsolationStateId = 18, Id = 17, Name = "In Place"},
                new IsolationState {IsolationMethodId = 9, IsolationStateId = 17, Id = 18, Name = "Removed"},
                new IsolationState {IsolationMethodId = 10, IsolationStateId = 20, Id = 19, Name = "Inserted"},
                new IsolationState {IsolationMethodId = 10, IsolationStateId = 19, Id = 20, Name = "Removed"},
                new IsolationState {IsolationMethodId = 11, IsolationStateId = 22, Id = 21, Name = "Disconnected"},
                new IsolationState {IsolationMethodId = 11, IsolationStateId = 21, Id = 22, Name = "Reconnected"},
                new IsolationState {IsolationMethodId = 12, IsolationStateId = 24, Id = 23, Name = "Labeled"},
                new IsolationState {IsolationMethodId = 12, IsolationStateId = 23, Id = 24, Name = "Label Removed"},

                new IsolationState {IsolationMethodId = 13, IsolationStateId = 26, Id = 25, Name = "Disconnected"},
                new IsolationState {IsolationMethodId = 13, IsolationStateId = 25, Id = 26, Name = "Reconnected"},
                new IsolationState {IsolationMethodId = 14, IsolationStateId = 28, Id = 27, Name = "Applied"},
                new IsolationState {IsolationMethodId = 14, IsolationStateId = 27, Id = 28, Name = "Removed"},
                new IsolationState {IsolationMethodId = 15, IsolationStateId = 30, Id = 29, Name = "Removed"},
                new IsolationState {IsolationMethodId = 15, IsolationStateId = 29, Id = 30, Name = "Replaced"},
                new IsolationState {IsolationMethodId = 16, IsolationStateId = 32, Id = 31, Name = "Open"},
                new IsolationState {IsolationMethodId = 16, IsolationStateId = 31, Id = 32, Name = "Closed"},
                new IsolationState {IsolationMethodId = 17, IsolationStateId = 34, Id = 33, Name = "Removed"},
                new IsolationState {IsolationMethodId = 17, IsolationStateId = 33, Id = 34, Name = "Replaced"},

                new IsolationState {IsolationMethodId = 18, IsolationStateId = 36, Id = 35, Name = "Disconnected"},
                new IsolationState {IsolationMethodId = 18, IsolationStateId = 35, Id = 36, Name = "Reconnected"},
                new IsolationState {IsolationMethodId = 19, IsolationStateId = 38, Id = 37, Name = "Open"},
                new IsolationState {IsolationMethodId = 19, IsolationStateId = 37, Id = 38, Name = "Closed"},
                new IsolationState {IsolationMethodId = 20, IsolationStateId = 40, Id = 39, Name = "Removed"},
                new IsolationState {IsolationMethodId = 20, IsolationStateId = 39, Id = 40, Name = "Replaced"},
                new IsolationState {IsolationMethodId = 21, IsolationStateId = 42, Id = 41, Name = "Open"},
                new IsolationState {IsolationMethodId = 21, IsolationStateId = 41, Id = 42, Name = "Closed"},

                new IsolationState {IsolationMethodId = 22, IsolationStateId = 44, Id = 43, Name = "Inhibited"},
                new IsolationState {IsolationMethodId = 22, IsolationStateId = 43, Id = 44, Name = "Reinstated"},
                new IsolationState {IsolationMethodId = 23, IsolationStateId = 46, Id = 45, Name = "Removed"},
                new IsolationState {IsolationMethodId = 23, IsolationStateId = 45, Id = 46, Name = "Replaced"},
                new IsolationState {IsolationMethodId = 24, IsolationStateId = 48, Id = 47, Name = "On"},
                new IsolationState {IsolationMethodId = 24, IsolationStateId = 47, Id = 48, Name = "Off"},
                new IsolationState {IsolationMethodId = 25, IsolationStateId = 50, Id = 49, Name = "OffScan"},
                new IsolationState {IsolationMethodId = 25, IsolationStateId = 49, Id = 50, Name = "OnScan"},
                new IsolationState {IsolationMethodId = 26, IsolationStateId = 52, Id = 51, Name = "Open"},
                new IsolationState {IsolationMethodId = 26, IsolationStateId = 51, Id = 52, Name = "Closed"},
            };
            return isolationStates;
        }

    }
}