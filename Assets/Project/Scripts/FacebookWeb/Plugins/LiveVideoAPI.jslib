mergeInto(LibraryManager.library, {
  SubscribeToReactions: function (liveVideo, token) {
    var liveVideoId = Pointer_stringify(liveVideo);
    var accessToken = Pointer_stringify(token);
    var source = new EventSource(
      "https://streaming-graph.facebook.com/" +
        liveVideoId +
        "/live_reactions?access_token=" +
        accessToken +
        "&fields=reaction_stream"
    );
    console.log(source);
    source.onmessage = function (event) {
      unityInstance.SendMessage(
        "FB_Connection",
        "OnUserReaction",
        String(event)
      );
    };
  },

  SubscribeToComments: function (liveVideo, token) {
    var liveVideoId = Pointer_stringify(liveVideo);
    var accessToken = Pointer_stringify(token);
    var source = new EventSource(
      "https://streaming-graph.facebook.com/" +
        liveVideoId +
        "/live_comments?access_token=" +
        accessToken +
        "&comment_rate=one_hundred_per_second&fields=from{name,id},message"
    );
    console.log(source);
    source.onmessage = function (event) {
      unityInstance.SendMessage(
        "FB_Connection",
        "OnUserComment",
        String(event)
      );
    };
  },
});
