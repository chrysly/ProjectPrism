using UnityEngine;

public static class ColorUtils {

    public static Color GetColor(this EColor eColor) {
        int val = (int) eColor;
        return new Color((val & 1 << 2) >> 2,
                         (val & 1 << 1) >> 1,
                         val & 1, 1);
    }

    public static EColor Add(this EColor eColor, EColor oColor) {
        int val1 = (int) eColor,
            val2 = (int) oColor;
        return (EColor) (val1 | val2);
    }

    public static EColor Subtract(this EColor eColor, EColor oColor) {
        int val1 = (int) eColor,
            val2 = (int) oColor;
        return (EColor) (val1 & ~(val1 & val2));
    }
}