using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define {

    public static string MapName(int mapIndex) {
        switch (mapIndex) {
            case 0:
                return "Forest";
            case 1:
                return "Laboratory";
            case 2:
                return "MoonBase";
            case 3:
                return "City";
            case 4:
                return "Ocean";
        }
        return "";
    }

}
