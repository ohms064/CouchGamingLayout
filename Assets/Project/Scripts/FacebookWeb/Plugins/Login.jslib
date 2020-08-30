mergeInto(LibraryManager.library, {
  FBLogin: function Login() {
    FB.login(function (response) {
      if (response.authResponse) {
        //I'm guessing that this will be null when status is different than connected. https://developers.facebook.com/docs/facebook-login/web
        console.log("User logged in");
        unityInstance.SendMessage("FB_Connection", "OnSuccessLogin");
      } else {
        console.log("user cancelled login");
      }
    });
  },
});
