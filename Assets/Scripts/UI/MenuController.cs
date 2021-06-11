using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; }
    
    [SerializeField]
    private MenuType DefaultMenu;

    private List<Menu> Menus;
    private Hashtable MenusByType { get; set; }
    private Coroutine coroutine;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            MenusByType = new Hashtable();
            Menus = GetComponentsInChildren<Menu>(true).ToList();
            Menus.ForEach(page => page.gameObject.SetActive(false));

            foreach (Menu page in Menus)
            {
                if (MenuExists(page.Type))
                {
                    Debug.LogWarning("Strona tego typu ju¿ istnieje");
                    return;
                }

                MenusByType.Add(page.Type, page);
            }

            if (DefaultMenu != MenuType.None)
            {
                EnableMenu(DefaultMenu);
            }

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnableMenu(MenuType type)
    {
        if (type == MenuType.None)
        {
            return;
        }

        if (!MenuExists(type))
        {
            Debug.LogWarning("Strona, któr¹ chcesz w³¹czyæ nie istnieje");
            return;
        }


        Menu page = GetMenu(type);
        page.gameObject.SetActive(true);
        page.Animate(true);

    }

    public void DisableMenu(MenuType type)
    {
        if (type == MenuType.None)
        {
            return;
        }

        if (!MenuExists(type))
        {
            Debug.LogWarning("Strona, któr¹ chcesz wy³¹czyæ nie istnieje");
            return;
        }

        Menu page = GetMenu(type);

        if (page.gameObject.activeSelf)
        {
            page.Animate(false);
        }
    }

    public void DisableAndEnableMenu(MenuType disableType, MenuType enableType, bool waitForAnimationEnd = false)
    {
        Menu disablePage = GetMenu(disableType);

        DisableMenu(disableType);

        if (enableType != MenuType.None)
        {
            if (waitForAnimationEnd && disablePage.UseAnimation)
            {
                Menu enablePage = GetMenu(enableType);

                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }

                coroutine = StartCoroutine(WaitForMenuExit(enablePage, disablePage));
            }
            else
            {
                EnableMenu(enableType);
            }
        }
    }

    private IEnumerator WaitForMenuExit(Menu enablePage, Menu disablePage)
    {
        while(disablePage.TargetState != Menu.InitialState)
        {
            yield return null;
        }

        EnableMenu(enablePage.Type);
    }

    private Menu GetMenu(MenuType type)
    {
        if (!MenuExists(type))
        {
            Debug.LogWarning("Strona tego typu nie istnieje");
            return null;
        }

        return (Menu)MenusByType[type];
    }

    private bool MenuExists(MenuType type)
    {
        return MenusByType.ContainsKey(type);
    }
}
