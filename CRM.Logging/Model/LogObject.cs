namespace CRM.Logging.Model {
    /// <summary>
    /// The information of a custom log entry. 
    /// <para>we should supply unique property in object istead of plain properties. </para>
    /// </summary>
    public class LogObject {

        /* properties */
        /// <summary>
        /// User Name which who is logged in system.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// User IP Address currently calling action.
        /// </summary>
        public string UserIpAddress { get; private set; }

        /// <summary>
        /// Controller which is currently called.
        /// </summary>
        public string Controller { get; private set; }

        /// <summary>
        /// Action which is currently performed. 
        /// <para>We must writer in formated manner so it will help us to find log entry.</para>
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// User Comment. 
        /// <para>User Can Easily Find Log With User Comment.</para>
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Any Other Id For which we did not do any provision.
        /// </summary>
        public string Ident { get; set; }

        /* construction */
        /// <summary>
        /// Overridable Constructor for only required field
        /// </summary>
        public LogObject(string userName, string userIpAddress, string controller, string action, string commentText)
            : this(userName, userIpAddress, controller, action, commentText, null) { }
        public LogObject(string userName, string userIpAddress, string controller, string action, string commentText = null, string ident = null) {
            UserName = userName;
            UserIpAddress = userIpAddress;
            Controller = controller;
            Action = action;
            CommentText = commentText;
            Ident = ident;
        }

    }
}
