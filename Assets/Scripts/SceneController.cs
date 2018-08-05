﻿using System.Collections;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;

    void Start()
    {
        Vector3 startPos = originalCard.transform.position; //Положение первой карты. Положение остальных отсчитываем от этой позиции

        int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3}; //пары идентификаторов для всех четырех спрайтов с изображениями

        numbers = ShuffleArray(numbers); //перемеживаем

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card; //ссылка на контейнер для исходной карты или оее копий
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                } else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * gridCols + i; //получаем id из перемешанного списка, а не из случайных чисел
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z); //смещаем только по х и у. z - не меняется.
            }
        }
	}

    //Реализация алгоритма тасования Кнута
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }
}
