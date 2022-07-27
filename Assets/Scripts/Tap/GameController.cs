using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameBoard game_board;

    private Vector2Int current_selection;
    private bool something_selected = false;

    [SerializeField] private AudioClip select_sfx, deselct_sfx;
    [SerializeField] private AudioClip music;

    private void Start()
    {
        game_board = GetComponent<GameBoard>();
        GameManager.instance.sound_manager.PlayMusic(music);
    }

    public void MainDown(Vector2Int click_pos)
    {
        if(!something_selected)
        {
            GameObject piece = game_board.GetPieceAtCoords(click_pos);
            if(piece != null)
            {
                piece.GetComponent<SpriteRenderer>().color = Color.red;
                current_selection = click_pos;
                something_selected = true;

                GameManager.instance.sound_manager.PlaySFX(select_sfx);
            }
        }
        else
        {
            GameObject piece = game_board.GetPieceAtCoords(click_pos);
            if(click_pos == current_selection)
            {
                piece.GetComponent<SpriteRenderer>().color = Color.blue;
                something_selected = false;

                GameManager.instance.sound_manager.PlaySFX(deselct_sfx);
            }
            else if(piece == null)
            {
                GameObject selected = game_board.GetPieceAtCoords(current_selection);
                selected.GetComponent<SpriteRenderer>().color = Color.blue;
                something_selected = false;

                GameManager.instance.sound_manager.PlaySFX(deselct_sfx);
            }
            else
            {
                GameObject selected = game_board.GetPieceAtCoords(current_selection);
                selected.GetComponent<SpriteRenderer>().color = Color.blue;
                something_selected = false;

                piece.GetComponent<SpriteRenderer>().color = Color.red;
                current_selection = click_pos;
                something_selected = true;

                GameManager.instance.sound_manager.PlaySFX(select_sfx);
            }
        }

/*        GameObject piece = game_board.GetPieceAtCoords(click_pos);
        if (piece != null)
            piece.GetComponent<SpriteRenderer>().color = Color.red;*/


        
/*        for(int i=click_pos.x-1; i<click_pos.x+2; i++)
        {
            for(int j=click_pos.y-1; j<click_pos.y+2; j++)
            {
                GameObject piece = game_board.GetPieceAtCoords(new Vector2Int(i, j));
                if(piece != null)
                    piece.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }*/
    }

}
