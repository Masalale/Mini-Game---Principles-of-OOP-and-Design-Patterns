using System.Collections.Generic;
using UnityEngine;

public class Leaderboard
{
    private const int MaxEntries = 3;
    private const string CountKey = "BestTimeCount";
    private const string TimeKeyPrefix = "BestTime";

    private List<float> times = new List<float>();

    public Leaderboard()
    {
        Load();
    }

    public List<float> GetTimes()
    {
        // Copy so other scripts can't change the saved list
        return new List<float>(times);
    }

    public void AddTime(float time)
    {
        times.Add(time);
        InsertionSort(times);

        if (times.Count > MaxEntries)
        {
            times.RemoveAt(times.Count - 1);
        }
        Save();
    }

    // Algorithm: insertion sort, fastest time first.
    // The list is already sorted except the new time at the end, so this is close to O(n) here.
    private void InsertionSort(List<float> list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            float current = list[i];
            int j = i - 1;

            while (j >= 0 && list[j] > current)
            {
                list[j + 1] = list[j];
                j--;
            }
            list[j + 1] = current;
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt(CountKey, times.Count);
        for (int i = 0; i < times.Count; i++)
        {
            PlayerPrefs.SetFloat(TimeKeyPrefix + i, times[i]);
        }
        PlayerPrefs.Save();
    }

    private void Load()
    {
        int count = PlayerPrefs.GetInt(CountKey, 0);
        // Older saves may hold more times than the board shows now, so only load the top ones
        for (int i = 0; i < count && i < MaxEntries; i++)
        {
            times.Add(PlayerPrefs.GetFloat(TimeKeyPrefix + i));
        }
    }
}
