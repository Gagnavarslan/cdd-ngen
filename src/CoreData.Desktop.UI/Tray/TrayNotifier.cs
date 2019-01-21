using CoreData.Desktop.Common.Tray;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows;
using System.Windows.Input;

namespace CoreData.Desktop.UI.Tray
{
    public class TrayTooltipNotifier : ITrayTooltipNotifier
    {
        struct Notification
        {
            public Notification(string title, string text, ICommand click, Exception ex)
            {
                Title = title;
                Text = text;
                Exception = ex;
                Hit = click;
            }

            public string Title { get; }
            public string Text { get; }
            public ICommand Hit { get; } // ???: consider to use CommandTarget https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/commanding-overview#command-target
            public Exception Exception { get; }
        }

        private readonly TaskbarIcon _tray;
        private readonly AppTrayViewModel _trayData;
        private Notification? _notification;

        public TrayTooltipNotifier(TaskbarIcon tray, AppTrayViewModel trayData)
        {
            _tray = tray;
            _trayData = trayData;

            _tray.TrayBalloonTipClicked += NotificationClicked;
            _tray.TrayBalloonTipClosed += NotificationClosed;
            _tray.TrayToolTipClose += NotificationClosed;
        }
        private void NotificationClicked(object sender, RoutedEventArgs e) =>
            _notification?.Hit.Execute(e);
        private void NotificationClosed(object sender, RoutedEventArgs e) =>
            _notification = null;

        public void Info(string title, string text, ICommand hit = null, Exception ex = null)
        {
            var notification = new Notification(title, text, hit, ex);
            Show(notification, BalloonIcon.Info);
        }

        public void Warn(string title, string text, ICommand hit = null, Exception ex = null)
        {
            var notification = new Notification(title, text, hit, ex);
            Show(notification, BalloonIcon.Warning);
        }

        public void Error(string title, string text, ICommand hit = null, Exception ex = null)
        {
            var notification = new Notification(title, text, hit, ex);
            Show(notification, BalloonIcon.Error);
        }

        private void Show(Notification notification, BalloonIcon icon)
        {
            _tray.HideBalloonTip();
            _notification = notification;
            _tray.ShowBalloonTip(notification.Title, notification.Text, icon);
        }

        //public static void ShowBalloon(this TaskbarIcon sender,
        //    IconNotification notification, ITrayNotificationRepository notificationRepository)
        //{
        //    ShowSimpleBalloon(sender, notification, notificationRepository,
        //        () => sender.ShowBalloonTip(notification.Title, notification.Text, notification.Icon));
        //}

        //[Dispatched]
        //public static void ShowBalloon<TBalloon>(this TaskbarIcon sender,
        //    IViewFactory factory, NotificationViewModel notifyModel, ITrayNotificationRepository notificationRepository)
        //    where TBalloon : FrameworkElement, new()
        //{
        //    var balloon = factory.Create<TBalloon>(notifyModel);
        //    sender.ShowCustomBalloon(balloon, PopupAnimation.Fade, DefaultTimeout);
        //    notificationRepository.Add(notifyModel);
        //}

        //[Dispatched]
        //private static void ShowSimpleBalloon(this TaskbarIcon sender,
        //    NotificationViewModel notification, ITrayNotificationRepository notificationRepository,
        //    Action taskBarShowBalloon)
        //{
        //    var dispatcher = sender.GetDispatcher();

        //    if (!dispatcher.CheckAccess())
        //    {
        //        Action action = () => ShowSimpleBalloon(sender, notification, notificationRepository, taskBarShowBalloon);
        //        dispatcher.Invoke(DispatcherPriority.Normal, action);
        //        return;
        //    }

        //    taskBarShowBalloon();
        //    notificationRepository.Add(notification);
        //}
    }
}
