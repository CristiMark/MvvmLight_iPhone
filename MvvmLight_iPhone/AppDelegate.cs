﻿using Foundation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using Google.Maps;
using MvvmLight_iPhone.ViewModel;
using UIKit;

namespace MvvmLight_iPhone
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        const string MapsApiKey = "AIzaSyAlWtaoBaP3gvxIHd6ju_Xd9agMRin4fZ4";
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            MapServices.ProvideAPIKey(MapsApiKey);
            // MVVM Light's DispatcherHelper for cross-thread handling.
            DispatcherHelper.Initialize(app);

            // Illustrates how to use the Messenger by receiving a message
            // and sending a message back.
            Messenger.Default.Register<NotificationMessageAction<string>>(
                this,
                HandleNotificationMessage);

            // Configure and register the MVVM Light NavigationService
            var nav = new NavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => nav);

            nav.Initialize((UINavigationController)Window.RootViewController);
            nav.Configure(ViewModelLocator.SecondPageKey, "SecondPage");

            // Register the MVVM Light DialogService
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            return true;
        }

        private void HandleNotificationMessage(NotificationMessageAction<string> message)
        {
            // Execute a callback to send a reply to the sender.
            message.Execute("Success! (from AppDelegate.cs)");
        }
    }
}