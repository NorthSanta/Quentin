﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;
using UnityEngine.UI;

public class SuspectClass : MonoBehaviour
{
    public GameObject buttons;
    public GameObject indict;
    public GameObject indict2;
    public GameObject indict3;
    public PostProcessingProfile ppProfile;
    public List<GameObject> clues = new List<GameObject>();
    [SerializeField]
    bool innocent;
    public GameObject fader;
    private float opacity;
    Color c;
    public static bool picked;
    public bool jury;
    public GameObject copy;
    public bool isCopy;
    bool alreadyUsed;
    public GameObject box;
    public int id;
    public ClueMaterialManager suspectMaterials;
    private void Start()
    {
        
        indict.GetComponent<Button>().onClick.AddListener(inculp);
        opacity = 1.0f;
        c = new Color(0, 0, 0, 255);
        isCopy = false;
       
    }
    private void Update()
    {
        //if (jury && isCopy)
        //{
        //    jury = false;
        //    fader.SetActive(true);
        //    fader.GetComponent<Image>().color = c;
        //    c = new Color(c.r, c.g, c.b, opacity);
        //    opacity -= 0.2f * Time.deltaTime;
        //    if (opacity <= 0.0f)
        //    {
                
        //        fader.SetActive(false);
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.E) && picked && !jury)
        {
            indict.SetActive(false);
            indict2.SetActive(false);
            indict3.SetActive(false);
            Player_Interaction.vell.SetActive(true);
            Destroy(Player_Interaction.nou);
            ppProfile.depthOfField.enabled = false;
            ppProfile.vignette.enabled = false;
            picked = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && picked && !jury)
        {
            indict.SetActive(false);
            indict2.SetActive(false);
            indict3.SetActive(false);
            Player_Interaction.vell.SetActive(true);
            Destroy(Player_Interaction.nou);
            ppProfile.depthOfField.enabled = false;
            ppProfile.vignette.enabled = false;
            picked = false;
            
        }
        
    }
    public void inculp()
    {

        box.SetActive(true);
        //jury = true;

        if (innocent)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            alreadyUsed = true;
            //return false;
            return;

        }
        else
        {
            int total = 0;

            for (int i = 0; i < clues.Count; i++)
            {

                /*for (int j = 0; j < Clue_Manager.solution.Count; j++)
                {
                    if (Clue_Manager.solution[j] == clues[i].GetComponent<Clue_Index>().clueIndex)
                    {
                        print(Clue_Manager.solution[j]);
                        total++;
                    }
                }*/
                if (Clue_Manager.solution.Contains(clues[i].GetComponent<Clue_Index>().clueIndex))
                {
                    total++;
                    print(clues[i].GetComponent<Clue_Index>().clueIndex);
                }

            }
            if (total > 2)
            {
                // print("Culpable!");
                transform.GetChild(0).gameObject.SetActive(true);
                alreadyUsed = true;
                return;
                //return true;
            }
            // print("Inocente!");
            transform.GetChild(1).gameObject.SetActive(true);
        }
        MovementMIA.indict = true;
        alreadyUsed = true;
        //Player_Interaction.foundClues.Clear();
        return;
        //return false;
    }
    private void OnMouseDown()
    {
        picked = true;
        if (!alreadyUsed)
        {
            indict.SetActive(true);
            indict.GetComponent<Button>().onClick.AddListener(this.inculp);
        }
        
        ppProfile.depthOfField.enabled = true;
        ppProfile.vignette.enabled = true;

        Player_Interaction.vell = gameObject;
        
        Player_Interaction.nou = (GameObject)Instantiate(gameObject);
        copy = Player_Interaction.nou;
        copy.GetComponent<SuspectClass>().isCopy = true;
        copy.GetComponent<Renderer>().material = suspectMaterials.suspectMaterial[id];

        // GameObject copy = (GameObject)Instantiate(interact.collider.gameObject);
        Player_Interaction.nou.SetActive(true);
        Player_Interaction.nou.layer = 4;
        Player_Interaction.nou.transform.GetChild(0).gameObject.layer = 4;
        Player_Interaction.nou.transform.GetChild(1).gameObject.layer = 4;
        Player_Interaction.nou.GetComponent<BoxCollider>().enabled = false;
        // copy.GetComponent<BoxCollider>().enabled = false;

        Player_Interaction.nou.transform.parent = buttons.transform.parent;
        Player_Interaction.nou.transform.SetAsFirstSibling();
        Player_Interaction.nou.transform.localPosition = new Vector3(0, 0, 10);
        Player_Interaction.nou.transform.localRotation = Quaternion.Euler(0, 0, 0);
        Player_Interaction.nou.transform.localScale = new Vector3(1250, 2500, 100);
        Player_Interaction.vell.SetActive(false);
    }
}
