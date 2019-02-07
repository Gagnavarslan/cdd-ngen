using CoreData.Desktop.Common.Tray;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;

namespace CoreData.Desktop.UI.Tray
{
    public class TrayTooltipNotifier : ITrayTooltipNotifier
    {
        private readonly TaskbarIcon _tray;
        private Balloon? _balloon;

        public TrayTooltipNotifier(TaskbarIcon tray)
        {
            _tray = tray;
            _tray.TrayBalloonTipClicked += BalloonClicked;
            _tray.TrayBalloonTipClosed += BalloonClosed;
            _tray.TrayToolTipClose += BalloonClosed;
        }
        private void BalloonClicked(object sender, RoutedEventArgs e) => _balloon?.Hit?.Execute(e);
        private void BalloonClosed(object sender, RoutedEventArgs e) => _balloon = null;

        public void Info(Balloon balloon) => Show(balloon, BalloonIcon.Info);

        public void Warn(Balloon balloon) => Show(balloon, BalloonIcon.Warning);

        public void Error(Balloon balloon) => Show(balloon, BalloonIcon.Error);

        private void Show(Balloon balloon, BalloonIcon icon)
        {
            _tray.HideBalloonTip();
            _balloon = balloon;
            _tray.ShowBalloonTip(balloon.Title, balloon.Text, icon);
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
