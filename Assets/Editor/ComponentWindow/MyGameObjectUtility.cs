using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public static class MyGameObjectUtility
{
    // �ʏ�true�����w�肵�Ȃ��̂Ńf�t�H���g������true�ɂ��Ă܂�
    public static T[] GetComponentsInActiveScene<T>(bool includeInactive = true)
    {
        // Active��Scene��Root�ɂ���GameObject[]���擾����
        var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        // ��� IEnumerable<T>
        IEnumerable<T> resultComponents = (T[])Enumerable.Empty<T>();
        foreach (var item in rootGameObjects)
        {
            // includeInactive = true ���w�肷���GameObject���񊈐��Ȃ��̂�����擾����
            var components = item.GetComponentsInChildren<T>(includeInactive);
            resultComponents = resultComponents.Concat(components);
        }
        return resultComponents.ToArray();
    }

    // 1�����擾�������ꍇ�͂�����iGetComponentsInActiveScene�����ɂ��ď������̂ŏ���������ł��j
    public static T GetComponentInActiveScene<T>(bool includeInactive = true)
    {
        // Active��Scene��Root�ɂ���GameObject[]���擾����
        var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        // ��� IEnumerable<T>
        IEnumerable<T> resultComponents = (T[])Enumerable.Empty<T>();
        foreach (var item in rootGameObjects)
        {
            // includeInactive = true ���w�肷���GameObject���񊈐��Ȃ��̂�����擾����
            var components = item.GetComponentsInChildren<T>(includeInactive);
            resultComponents = resultComponents.Concat(components);
        }
        return resultComponents.First();
    }
}
