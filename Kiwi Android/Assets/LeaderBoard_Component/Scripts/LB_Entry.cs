
[System.Serializable]
public class Status {
    public int code;
    public string msg;
}

[System.Serializable]
public class RequestStatus {
    public Status status;
}


[System.Serializable]
public class LB_Entry {
    public int rank;
    public string name;
    public int points;
}

[System.Serializable]
public class LeaderboardResult {
    public LB_Entry[] entries;
}

