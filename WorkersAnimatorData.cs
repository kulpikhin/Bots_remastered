using UnityEngine;

public static class BotsAnimatorData
{
    public static class Params
    {
        public static int IsMove = Animator.StringToHash(nameof(IsMove));
        public static int IsMining = Animator.StringToHash(nameof(IsMining));
    }
}
