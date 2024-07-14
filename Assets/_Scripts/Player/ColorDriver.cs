using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(OrbHandler))]
public class ColorDriver : MonoBehaviour {
    [SerializeField] private MeshRenderer _renderer;
    private Material _rgbMat;
    private OrbHandler _orbHandler;

    private EColor _color;
    public EColor GetColor() { return _color;}

    [SerializeField] private float colorSwapRate = 0.2f;
    [SerializeField] private bool logToConsole;
    
    private void Awake() {
        _orbHandler = GetComponent<OrbHandler>();
        _orbHandler.OnInventoryOperation += AdjustRGBValues;
    }
    
    #region ColorCheck
    //TODO: Refactor with new custom color system, why are you looking at this u bastard
    //     ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣶⣄⡀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣤⣤⣤⣤⣤⣄⣀⡀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣾⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⡏⠙⢣⡄⢀⣤⡴⠿⠛⠋⠉⠀⠀⠀⠀⠀⠀⠈⠉⠻⢷⡦⣤⡀⠀⣴⠿⢻⣿⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣇⣠⣴⡿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⢧⡤⢼⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢷⣽⠟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠷⣄⠙⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⠿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢳⣦⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡾⠃⠀⠀⠀⠀⠀⠀⢀⣤⣴⣶⡲⠶⣦⣄⣀⡀⢀⣀⣴⣶⣶⣶⣤⣀⠀⠀⠀⠀⠀⠀⠙⢮⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠟⠁⠀⠀⠀⠀⠀⠀⣸⡟⠋⣽⣿⣿⣷⡄⠀⢹⠿⠻⡅⢀⣴⣿⣿⣯⠙⠻⣦⠀⠀⠀⠀⠀⠀⠳⡿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⡿⠘⠁⠀⠀⠀⠀⠀⠀⢻⣾⣆⠙⣿⣿⡿⠃⢠⡾⠀⠀⠻⣌⠛⢿⣿⡟⠃⢠⠇⠀⠀⠀⠀⠀⠀⠀⠙⣼⣤⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠿⠷⣶⣀⣠⣶⣿⣤⣤⣄⡀⠈⠳⢦⣤⣴⠶⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢎⢣⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⠿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⡿⠟⠉⠁⠀⠀⠉⠙⠷⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠳⣷⡄⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⡿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⣿⣀⣀⠀⠀⠀⠀⠀⠀⠀⣈⣻⣷⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⢦⡀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⣠⡾⡋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⠿⢿⣿⣻⣶⣆⣀⣴⡶⠿⣿⠿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣄⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⢠⣾⠋⠠⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⠛⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢧⡀⠀⠀⠀
    // ⠀⠀⠀⠀⢰⣿⡿⠀⢰⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣏⣇⠀⠀⠀
    // ⠀⠀⠀⠀⢸⣿⡀⢰⡟⠀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣴⡏⢿⡀⠀⠀
    // ⠀⠀⠀⠀⢸⣿⣿⡟⠀⠀⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡿⠀⢸⡇⠀⠀
    // ⠀⠀⠀⠀⣿⣿⣿⡇⠀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠀⠈⡇⠀⠀
    // ⠀⠀⠀⢠⣿⣿⣿⣧⠀⠸⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⠒⠒⠒⠒⠶⠶⠦⠤⠤⠤⢤⠄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣿⡆⠀⢹⠀⠀
    // ⠀⠀⠀⢸⣿⣿⣿⠹⣧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⠏⢹⣇⠀⢸⡇⠀
    // ⠀⠀⢀⣾⣿⣿⡏⠀⢻⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣶⡿⠀⠀⢻⣇⠀⣧⠀
    // ⠀⠀⢸⣿⣿⣿⠀⠀⠀⠻⣆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣤⣿⡿⠁⠀⠀⢸⣿⡆⢸⡄
    // ⠀⠀⣸⡟⢿⣿⠀⠀⠀⠀⠙⢷⣀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣴⣶⣿⡿⠁⠀⠀⠀⠀⣿⡿⢞⡇
    // ⠀⠀⣿⠃⣿⣿⠀⠀⠀⠀⠀⠀⠻⢿⣧⣀⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣤⣴⣾⣿⣿⡿⠋⠀⠀⠀⠀⠀⠀⣿⡇⠈⡇
    // ⠀⠀⣿⢀⣿⡏⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣶⣄⠀⢀⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠿⣿⣿⡿⠋⠀⠀⠀⠀⠀⠀⠀⢸⣿⠀⠀⠇
    // ⠀⢀⣿⢈⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⣿⣿⣷⣶⣭⣅⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣴⣿⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⡏⠀⠀⡀
    // ⠀⢸⡟⢸⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⡿⠈⣿⣿⡏⠛⠿⠶⣦⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣤⡶⠶⣿⣿⣿⣿⠿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⢸⡇⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⡇⢠⣿⣿⠇⠀⠀⠀⠀⠉⠉⠙⠛⠛⠶⠶⣶⡶⡶⠶⠞⠛⠛⠉⠉⠀⠀⠀⢹⢿⣿⠟⠈⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⢸⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⡇⢸⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⡇⠀⢰⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⣸⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⡇⣸⣿⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⣿⠀⣸⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⢰⣿⣏⠙⢷⣄⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⣧⣿⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⣿⡆⢹⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠀⠀⠀⠀
    // ⢸⣿⣿⠁⣸⠻⣆⠀⠀⠀⠀⠀⠀⠀⠸⣿⣾⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⣧⣾⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡞⠁⠀⠀⠀⠀
    // ⣿⣿⣿⠀⢿⣷⣿⡄⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀⢠⠟⠀⠀⠀⠀⠀⠀
    // ⣿⣿⣿⠀⠘⡿⢿⣿⣦⠀⠀⠀⠀⠀⠀⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⡏⣿⡇⠀⠀⠀⠀⠀⠀⠀⢠⠟⣀⡴⣦⠀⠀⠀⢀
    // ⢸⣿⣿⣧⣼⣄⠈⣿⣿⡆⠀⠀⠀⠀⠀⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⠁⣿⡇⠀⠀⠀⠀⠀⠀⠀⣿⣾⠋⠀⢸⠀⠀⠀⣾
    // ⠈⢻⣿⣿⣯⣿⣿⠿⣿⠇⠀⠀⠀⠀⠀⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⢐⣿⠃⠀⠀⠀⠀⠀⠀⣸⣿⠟⠀⢀⣇⣰⠟⣸⣿
    // ⠀⠀⠘⠻⣿⣷⣝⢻⡟⠀⠀⠀⠀⠀⠀⣿⡏⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⢸⣿⡄⠀⠀⠀⠀⠀⠀⠘⢁⣀⣘⣿⣿⡟⣰⣿⡟
    // ⠀⠀⠀⠀⠘⠛⠛⠛⠋⠀⠀⠀⠀⠀⢀⣿⣧⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⣿⣿⣿⠇⠀⠀⠀⠀⠀⠰⣤⣬⣭⣬⣿⣿⡿⠛⠁⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⡿⢿⡿⣇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⠋⠙⣿⠀⠀⠀⠀⠀⠀⠀⠈⠉⠉⠉⠉⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⡷⠨⣵⣜⢦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⡾⠇⠀⠀⠈⠻⢶⣦⣄⣀⣤⣤⣤⣤⣄⣀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣶⠻⣿⣡⣤⠀⠈⣻⣷⣮⣟⣲⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣿⡛⠋⢨⣽⠻⣿⡦⣄⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⡟⣹⣯⣿⣿⣿⡏⢹⣿⣿⣿⣿⣏⢹⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠛⠻⢶⣶⣖⡰⠇⢿⡄⢹⣷⣿⣝⠿⣄⢸⣿⡿⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⣷⣿⣿⡿⠿⣿⣧⣼⣿⠿⢿⣿⣿⣶⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⠿⠷⠿⠾⠻⠿⠿⠿⠛⠉⠁⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠁⠀⠀⠈⡉⠉⠁⠀⠀⢈⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⣀⠀⠀⠀⢀⣀⠀⠀⠀⠀⠀⠀⠀
    private EColor AdjustEColor(Vector3 color) {

        if (color == Vector3.one) {
            return EColor.White;
        }

        if (color == Vector3.zero) {
            return EColor.Black;
            
        }
        
        if (color == new Vector3(1, 0, 0)) return EColor.Red;
        if (color == new Vector3(0, 1, 0)) return EColor.Green;
        if (color == new Vector3(0, 0, 1)) return EColor.Blue;
        if (color == new Vector3(1, 1, 0)) return EColor.Yellow;
        if (color == new Vector3(0, 1, 1)) return EColor.Cyan;
        if (color == new Vector3(1, 0, 1)) return EColor.Magenta;

        return EColor.White;
    }
    #endregion ColorCheck

    private void AdjustRGBValues(OrbThrow[] orbs) {
        Vector3 rgb = Vector3.zero;
        for (int i = 0; i < orbs.Length; i++) {
            if (orbs[i] != null) {
                switch (orbs[i].GetOrbColor()) {
                    case EColor.Red:
                        rgb += new Vector3(1f, 0f, 0f);
                        break;
                    case EColor.Green:
                        rgb += new Vector3(0f, 1f, 0f);
                        break;
                    case EColor.Blue:
                        rgb += new Vector3(0f, 0f, 1f);
                        break;
                }
            }
        }

        _color = AdjustEColor(rgb);
        if (logToConsole) Debug.Log(_color.ToString());

        MaterialPropertyBlock mpb = new ();
        _renderer.GetPropertyBlock(mpb);
        // DOTween.To(() => mpb.GetFloat("_RStrength"), x => mpb.SetFloat("_RStrength", x), rgb.x, colorSwapRate);
        // DOTween.To(() => mpb.GetFloat("_GStrength"), x => mpb.SetFloat("_GStrength", x), rgb.y, colorSwapRate);
        // DOTween.To(() => mpb.GetFloat("_BStrength"), x => mpb.SetFloat("_BStrength", x), rgb.z, colorSwapRate);
        
        DOVirtual.Float(0f, rgb.x, colorSwapRate, (float value) => {
            mpb.SetFloat("_RStrength", value);
            _renderer.SetPropertyBlock(mpb);
        });
        
        DOVirtual.Float(0f, rgb.y, colorSwapRate, (float value) => {
            mpb.SetFloat("_GStrength", value);
            _renderer.SetPropertyBlock(mpb);
        });
        
        DOVirtual.Float(0f, rgb.z, colorSwapRate, (float value) => {
            mpb.SetFloat("_BStrength", value);
            _renderer.SetPropertyBlock(mpb);
        });
    }
}
