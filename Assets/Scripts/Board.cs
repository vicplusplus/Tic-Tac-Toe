using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    private bool?[] board;
    [SerializeField] private bool player;
    [SerializeField] private int SIZE;
    [SerializeField] private GameObject[] buttons;

    private void Reset()
    {
        board = new bool?[SIZE * SIZE];
    }

    private void Start()
    {
        Reset();
        if (board.Length != buttons.Length)
        {
            Debug.LogError("Buttons and Board should be of equal length!\nBoard may not work as intended.");
        }
    }

    private bool Evaluate(bool player)
    {
        // handle diagonals in outer loop
        bool d1 = true;
        bool d2 = true;
        for (int i = 0; i < SIZE; i++)
        {
            // handle rows/columns in inner loop
            bool row = true;
            bool col = true;
            for (int j = 0; j < SIZE; j++)
            {
                if (board[i * SIZE + j] != player) row = false;
                if (board[i + j * SIZE] != player) col = false;
            }
            if (row || col) return true;

            if (board[i * SIZE + i] != player) d1 = false;
            if (board[i * SIZE + SIZE - i - 1] != player) d2 = false;
        }
        return d1 || d2;
    }

    public void Set(int pos)
    {
        if(board[pos] != null)
        {
            Debug.LogWarning("Tried to place in a filled square!");
            return;
        }
        if(pos >= buttons.Length || pos >= SIZE * SIZE)
        {
            Debug.LogError("Invalid button index!");
            return;
        }
        board[pos] = player;
        buttons[pos].GetComponent<Button>().interactable = false;
    }
}
