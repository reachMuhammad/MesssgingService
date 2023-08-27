using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class DOTweenExt
{

    public static Tween DOInvoke(this Object targetObject, float delay,  TweenCallback callback)
    {
        float temp = 0;
        return DOTween.To(() => temp, (intermVal) => temp = intermVal, 5f, delay).OnComplete(callback).SetTarget(targetObject);
    }
    public static Tween DOInt(this Object targetObject, int startValue, int endValue, float time, TweenCallback<int> onUpdate)
    {
        int temp = startValue;
        return DOTween.To(() => temp, (intermVal) => temp = intermVal, endValue, time).OnUpdate(() => onUpdate?.Invoke(temp)).SetTarget(targetObject);
    }
    public static Tween DOFloat(float from, float to, float duration, TweenCallback<float> onVirtualUpdate)
    {
        var obj = new GameObject("Float Tween");
        obj.hideFlags = HideFlags.HideInHierarchy;
        return obj.DOFloat(from, to, duration, onVirtualUpdate).OnComplete(()=>{
            Object.Destroy(obj);
        });
    }

    public static Tween DOFloat(this Object targetObject, float startValue, float endValue, float time)
    {
        float temp = startValue;
        return DOTween.To(() => temp, (intermVal) => temp = intermVal, endValue, time).SetTarget(targetObject);
    }

    public static Tween DOFloat(this Object targetObject, float startValue, float endValue, float time, TweenCallback<float> onUpdate)
    {
        float temp = startValue;
        return DOTween.To(() => temp, (intermVal) => temp = intermVal, endValue, time).OnUpdate(()=> onUpdate?.Invoke(temp)).SetTarget(targetObject);
    }

    public static Tween DOVector2(this Object targetObject, Vector2 startValue, Vector2 endValue, float time, TweenCallback<Vector2> onUpdate)
    {
        Vector2 temp = startValue;
        return DOTween.To(() => temp, (intermVal) => temp = intermVal, endValue, time).OnUpdate(() => onUpdate?.Invoke(temp)).SetTarget(targetObject);
    }
    public static Tween DOVector3(this Object targetObject, Vector3 startValue, Vector3 endValue, float time, TweenCallback<Vector3> onUpdate)
    {
        Vector3 temp = startValue;
        return DOTween.To(() => temp, (intermVal) => temp = intermVal, endValue, time).OnUpdate(() => onUpdate?.Invoke(temp)).SetTarget(targetObject);
    }

    //}
    // public static Tween DOValue<T>(this Object targetObject, T startValue, T endValue, float time, TweenCallback<T> onUpdate)
    // {
    //     float temp = 0;
    //     return DOTween.To(() => temp, (intermVal) => temp = intermVal, 1f, time).OnUpdate(() => onUpdate?.Invoke());
    // }
}
