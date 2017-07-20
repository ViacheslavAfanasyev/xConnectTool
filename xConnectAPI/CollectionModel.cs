using System;
using Sitecore.XConnect.Operations;
using Sitecore.XConnect.Schema;

namespace Sitecore.XConnect.Collection.Model
{

    public static class CollectionModel
    {
        public static XdbModel Model { get; } = BuildCoreModel();


        static XdbModel BuildCoreModel()
        {
            var builder = new XdbModelBuilder("Sitecore.XConnect.Collection.Model", new XdbModelVersion(8, 3));

            //Contact facets
            builder.DefineFacet<Contact, EmailAddressList>(FacetKeys.EmailAddressList);
            builder.DefineFacet<Contact, PersonalInformation>(FacetKeys.PersonalInformation);
            builder.DefineFacet<Contact, AddressList>(FacetKeys.AddressList);
            builder.DefineFacet<Contact, Avatar>(FacetKeys.Avatar);
            builder.DefineFacet<Contact, Classification>(FacetKeys.Classification);
            builder.DefineFacet<Contact, PhoneNumberList>(FacetKeys.PhoneNumberList);
            builder.DefineFacet<Contact, ListSubscriptions>(FacetKeys.ListSubscriptions);
            builder.DefineFacet<Contact, ConsentInformation>(FacetKeys.ConsentInformation);
            builder.DefineFacet<Contact, AutomationPlanExit>(FacetKeys.AutomationPlanExit);
            builder.DefineFacet<Contact, AutomationPlanEnrollmentCache>(FacetKeys.AutomationPlanEnrollmentCache);
            builder.DefineFacet<Contact, MergeInfo>(FacetKeys.MergeInfo);

            //Calculated facets
            builder.DefineFacet<Contact, KeyBehaviorCache>(FacetKeys.KeyBehaviorCache);
            builder.DefineFacet<Contact, EngagementMeasures>(FacetKeys.EngagementMeasures);
            builder.DefineFacet<Contact, ContactBehaviorProfile>(FacetKeys.ContactBehaviorProfile);

            //Interaction facets
            builder.DefineFacet<Interaction, WebVisit>(FacetKeys.WebVisit);
            builder.DefineFacet<Interaction, IpInfo>(FacetKeys.IpInfo);
            builder.DefineFacet<Interaction, ProfileScores>(FacetKeys.ProfileScores);
            builder.DefineFacet<Interaction, LocaleInfo>(FacetKeys.LocaleInfo);
            builder.DefineFacet<Interaction, UserAgentInfo>(FacetKeys.UserAgentInfo);

            //Events
            builder.DefineEventType<PageViewEvent>(true);
            builder.DefineEventType<CampaignEvent>(false);
            builder.DefineEventType<DownloadEvent>(false);
            builder.DefineEventType<SearchEvent>(false);

            return builder.BuildModel();
        }

        public static SetFacetOperation<EmailAddressList> SetEmails(this IXdbContext context, IEntityReference<Contact> contact, EmailAddressList facet)
        {
            return context.SetFacet(new FacetReference(contact, FacetKeys.EmailAddressList), facet);
        }

        public static EmailAddressList Emails(this Contact c)
        {
            return c.GetFacet<EmailAddressList>(FacetKeys.EmailAddressList);
        }

        public static SetFacetOperation<Classification> SetClassification(this IXdbContext context, IEntityReference<Contact> contact, Classification facet)
        {
            return context.SetFacet(new FacetReference(contact, FacetKeys.Classification), facet);
        }

        public static Classification Classification(this Contact c)
        {
            return c.GetFacet<Classification>(FacetKeys.Classification);
        }

        public static SetFacetOperation<PersonalInformation> SetPersonal(this IXdbContext context, IEntityReference<Contact> contact, PersonalInformation facet)
        {
            return context.SetFacet(new FacetReference(contact, FacetKeys.PersonalInformation), facet);
        }

        public static PersonalInformation Personal(this Contact c)
        {
            return c.GetFacet<PersonalInformation>(FacetKeys.PersonalInformation);
        }

        public static SetFacetOperation<AddressList> SetAddresses(this IXdbContext context, IEntityReference<Contact> contact, AddressList facet)
        {
            return context.SetFacet(new FacetReference(contact, FacetKeys.AddressList), facet);
        }

        public static AddressList Addresses(this Contact c)
        {
            return c.GetFacet<AddressList>(FacetKeys.AddressList);
        }

        public static SetFacetOperation<Avatar> SetAvatar(this IXdbContext context, IEntityReference<Contact> contact, Avatar facet)
        {
            return context.SetFacet(new FacetReference(contact, FacetKeys.Avatar), facet);
        }

        public static Avatar Avatar(this Contact c)
        {
            return c.GetFacet<Avatar>(FacetKeys.Avatar);
        }

        //No set method. You are not normally supposed to set a calculated facet
        public static KeyBehaviorCache KeyBehaviorCache(this Contact c)
        {
            return c.GetFacet<KeyBehaviorCache>(FacetKeys.KeyBehaviorCache);
        }

        //No set method. You are not supposed to set a calculated facet
        public static EngagementMeasures EngagementMeasures(this Contact c)
        {
            return c.GetFacet<EngagementMeasures>(FacetKeys.EngagementMeasures);
        }


        public static SetFacetOperation<PhoneNumberList> SetPhoneNumbers(this IXdbContext context, IEntityReference<Contact> contact, PhoneNumberList facet)
        {
            return context.SetFacet(new FacetReference(contact, FacetKeys.PhoneNumberList), facet);
        }

        public static PhoneNumberList PhoneNumbers(this Contact c)
        {
            return c.GetFacet<PhoneNumberList>(FacetKeys.PhoneNumberList);
        }

        public static SetFacetOperation<ListSubscriptions> SetListSubscriptions(this IXdbContext context, IEntityReference<Contact> contact, ListSubscriptions facet)
        {
            return context.SetFacet(new FacetReference(contact, FacetKeys.ListSubscriptions), facet);
        }

        public static ListSubscriptions ListSubscriptions(this Contact c)
        {
            return c.GetFacet<ListSubscriptions>(FacetKeys.ListSubscriptions);
        }

        public static SetFacetOperation<ConsentInformation> SetConsentInformation(this IXdbContext context, IEntityReference<Contact> contact, ConsentInformation facet)
        {
            return context.SetFacet(new FacetReference(contact, FacetKeys.ConsentInformation), facet);
        }

        public static ConsentInformation ConsentInformation(this Contact c)
        {
            return c.GetFacet<ConsentInformation>(FacetKeys.ConsentInformation);
        }

        public static SetFacetOperation<AutomationPlanExit> SetAutomationPlanExit(this IXdbContext context, IEntityReference<Contact> contact, AutomationPlanExit facet)
        {
            return context.SetFacet(new FacetReference(contact, FacetKeys.AutomationPlanExit), facet);
        }

        public static AutomationPlanExit AutomationPlanExit(this Contact c)
        {
            return c.GetFacet<AutomationPlanExit>(FacetKeys.AutomationPlanExit);
        }

        public static SetFacetOperation<AutomationPlanEnrollmentCache> SetAutomationPlanEnrollmentCache(this IXdbContext context, IEntityReference<Contact> contact, AutomationPlanEnrollmentCache facet)
        {
            return context.SetFacet(new FacetReference(contact, FacetKeys.AutomationPlanEnrollmentCache), facet);
        }

        public static MergeInfo MergeInfo(this Contact c)
        {
            return c.GetFacet<MergeInfo>(FacetKeys.MergeInfo);
        }

        public static SetFacetOperation<MergeInfo> SetMergeInfo(this IXdbContext context, IEntityReference<Contact> contact, MergeInfo facet)
        {
            return context.SetFacet(new FacetReference(contact, FacetKeys.MergeInfo), facet);
        }

        public static AutomationPlanEnrollmentCache AutomationPlanEnrollmentCache(this Contact c)
        {
            return c.GetFacet<AutomationPlanEnrollmentCache>(FacetKeys.AutomationPlanEnrollmentCache);
        }

        public static WebVisit WebVisit(this Interaction interaction)
        {
            return interaction.GetFacet<WebVisit>(FacetKeys.WebVisit);
        }

        public static void SetWebVisit(this IXdbContext context, Interaction interaction, WebVisit webVisit)
        {
            context.SetFacet(new FacetReference(interaction, FacetKeys.WebVisit), webVisit);
        }

        public static IpInfo IpInfo(this Interaction interaction)
        {
            return interaction.GetFacet<IpInfo>(FacetKeys.IpInfo);
        }

        public static void SetIpInfo(this IXdbContext context, Interaction interaction, IpInfo ipInfo)
        {
            context.SetFacet(new FacetReference(interaction, FacetKeys.IpInfo), ipInfo);
        }

        //No set method. You are not normally supposed to set a calculated facet
        public static ContactBehaviorProfile ContactBehaviorProfile(this Contact contact)
        {
            return contact.GetFacet<ContactBehaviorProfile>(FacetKeys.ContactBehaviorProfile);
        }

        public static ProfileScores ProfileScores(this Interaction interaction)
        {
            return interaction.GetFacet<ProfileScores>(FacetKeys.ProfileScores);
        }

        public static void SetProfileScores(this IXdbContext context, Interaction interaction, ProfileScores profileScores)
        {
            context.SetFacet(new FacetReference(interaction, FacetKeys.ProfileScores), profileScores);
        }


        public static UserAgentInfo UserAgentInfo(this Interaction interaction)
        {
            return interaction.GetFacet<UserAgentInfo>(FacetKeys.UserAgentInfo);
        }

        public static void SetUserAgentInfo(this IXdbContext context, Interaction interaction, UserAgentInfo userAgentInfo)
        {
            context.SetFacet(new FacetReference(interaction, FacetKeys.UserAgentInfo), userAgentInfo);
        }

        public static LocaleInfo LocaleInfo(this Interaction interaction)
        {
            return interaction.GetFacet<LocaleInfo>(FacetKeys.LocaleInfo);
        }

        public static void SetLocaleInfo(this IXdbContext context, Interaction interaction, LocaleInfo localeInfo)
        {
            context.SetFacet(new FacetReference(interaction, FacetKeys.LocaleInfo), localeInfo);
        }

        public static class FacetKeys
        {
            public const string Avatar = nameof(CollectionModel.Avatar);
            public const string Classification = nameof(CollectionModel.Classification);
            public const string EmailAddressList = nameof(CollectionModel.Emails);
            public const string PersonalInformation = nameof(CollectionModel.Personal);
            public const string AddressList = nameof(CollectionModel.Addresses);
            public const string PhoneNumberList = nameof(CollectionModel.PhoneNumbers);
            public const string ListSubscriptions = nameof(CollectionModel.ListSubscriptions);
            public const string ConsentInformation = nameof(CollectionModel.ConsentInformation);
            public const string AutomationPlanExit = nameof(CollectionModel.AutomationPlanExit);
            public const string AutomationPlanEnrollmentCache = nameof(CollectionModel.AutomationPlanEnrollmentCache);
            public const string MergeInfo = nameof(MergeInfo);

            public const string WebVisit = nameof(CollectionModel.WebVisit);
            public const string IpInfo = nameof(CollectionModel.IpInfo);
            public const string ContactBehaviorProfile = nameof(CollectionModel.ContactBehaviorProfile);
            public const string ProfileScores = nameof(CollectionModel.ProfileScores);

            public const string KeyBehaviorCache = nameof(CollectionModel.KeyBehaviorCache);
            public const string EngagementMeasures = nameof(CollectionModel.EngagementMeasures);

            public const string LocaleInfo = nameof(CollectionModel.LocaleInfo);
            public const string UserAgentInfo = nameof(UserAgentInfo);
        }
    }
}
