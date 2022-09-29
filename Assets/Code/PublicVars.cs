public static class PublicVars
{
    static int score = 0;
    static int count_monster = 0;

    public static void AddScore(int points){
        if (points == 0){
            score = 0;
        } else {
            score+=points;
        }
    }
    public static void AddKill(int points){
        count_monster+=points;
    }
}
