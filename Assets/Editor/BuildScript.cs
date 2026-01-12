using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Список сцен
        // ========================
        string[] scenes = {
        "Assets/Scenes/Game.unity",
        };

        // ========================
        // Пути к файлам сборки
        // ========================
        string aabPath = "HappyChickens.aab";
        string apkPath = "HappyChickens.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64 = "MIIJ5gIBAzCCCZAGCSqGSIb3DQEHAaCCCYEEggl9MIIJeTCCBbAGCSqGSIb3DQEHAaCCBaEEggWdMIIFmTCCBZUGCyqGSIb3DQEMCgECoIIFQDCCBTwwZgYJKoZIhvcNAQUNMFkwOAYJKoZIhvcNAQUMMCsEFGXNTsmO0wPGYOQjf166VR9+zz5tAgInEAIBIDAMBggqhkiG9w0CCQUAMB0GCWCGSAFlAwQBKgQQKJUZ89NNbiFbD9420mRE/ASCBNBVoZkHtO4BGryG2IJi4UDHL79XIKTUvSHbinV8XMg/BrAkl0tHAxJZ3rMcNSIrFFnoSR86RcSCI4vK76d0sd3wW3Rwv9mA9ArWW454W0utNOy4gAAziSJnWGooGh+m+XSFY9lBVsx9d5VmhXDZ0WkCpFilApn1iAti/b5B+D5JqcK/q/OCIF4bmFH4iOsUDLGsTjMPnAcvzPdSjmrxuadYKCbavbZJL9c+Eo6HurNWGBN/LJZPcubjraV/M/JI3o0uhpminLdU4d+CO4UWWYfYA0G5TTg46b257GV5rXvCmpTvx9uMTVd6+r49oxKrXNryvPwWUFT37KNAXVyXop9ZzS5VXiBHba6Xv4dR7quxXF0N0xdXcUgC2f+hquZlL6h3WM6pBayTOscQgkdwNr9Vc+7boMUr6P/mda26Gq0xwgAfyE96OZ8uQ4jgH8G5LIy29PEhWFVb5shSMQidCpf37l63nxXdUw516jBrU53OPWyPKCyk7z9g0gh8QhVZkUKdB8V+AYIimTPJcNVmdO+f76SLEx4l8WxsQ+oHVKyMRtMVTg2st5XaCllqumImWjYJ95iex0QZSfaGeSHY4rYVaXfvM1kg3Fr7M8ugjX3bIhhDQ0RkoNrHVJSdgEpTQ5TG62FFTHZgTXjbmUh7zxwJOKuD7xGzVCtM8Fih72R/f9Ve+Y/OYI7Vlzu6XoBGQvzQBjUOG3fP0RBr71Cg87m+c/Lqw+qT0tf6X6Bghev8TUDxmOGCv/Q2+6FiFkoTndowzi6CMLaW2zaKKa64A5Nd60IRRF0HY7LxL8ucIZLOQ0cItAw2yP6SIa6swzePGYFM/4sjkj4YeBKVa5SRU+dZH/L9VfYgOCtI/N7v74OdTNOWlt5hK8hZPvVqe5MkOmUd9FnJX5AAYVnafUFiG/Q+4bC10Ghb5cZ60arTmkNUIqrOtyUlxRrolV9B6gAE1JZnhr7cLnXOQrSnV3eCjuCWXiO3Wr9e/yeK2VSE4hlrY4vRmvYne5xP0IAYPzmQ/JEmkQkfk5YmHA8N/Lw18v+qm+DSkb5n6YH6MZTt2Xje0qGnEgt2T41h0VPQLa+1z8UjlIaYIBK2jrW+J+Cn4bSKjx5JRJJiFDz0BaCVG1kb6uY5L2NxPwNONzyH8CxPGbo9H3fGwZB1qbV7Xl1YpFaIcL0hVDLopAeCeg0adS5IcjeKFxHxinc2e/fCIbcSmWOsBsET0ONG1tqhHb3LJ4t76eafBv3t5ASkG/MS46DY9PbwSs74dxn2lpJKCheYqwcXmHb8uNfs3aY82pllL+/5iokLufaJaH1PjvJe8A8zwjztPSSODt/qOSluqpFJEw1ZS5sCi6QDUZ3NsZloGPdfgTpdMsJlBFr9DRf81mwkhH5h8nEy9O0AteutNKoVI2PbsoF9YGkr4yu/JmKiUy5wi154N2KbJGWd2IpTySgy/tqPN3SDTU6h3liXp25KYWKfTlWp2nbCTkgYUvicgDaOnGDwW8GV3gkqYHNOb43H1jJ7/GG7uxFbw1HQvS3GNwYKtJIJOEGXSAWs/aVYnzivnblcRi9ktQbhJazkHXEvlpieOt4tUF0oHMKqDbmt6gsaIox0TmZ6/0wuLPj+WVq3DkCkkyb+oxybP3ITSq7SYjFCMB0GCSqGSIb3DQEJFDEQHg4AawBpAHQAYwBoAGUAbjAhBgkqhkiG9w0BCRUxFAQSVGltZSAxNzY4MjMwODAyMDk5MIIDwQYJKoZIhvcNAQcGoIIDsjCCA64CAQAwggOnBgkqhkiG9w0BBwEwZgYJKoZIhvcNAQUNMFkwOAYJKoZIhvcNAQUMMCsEFAT4CxlHjfZk7/Yg0PK2+C3klhpMAgInEAIBIDAMBggqhkiG9w0CCQUAMB0GCWCGSAFlAwQBKgQQIN47FYOYDXOYc27NyyL5poCCAzBRzB/wggyYM/3j9yWSQtWfcNtBkiDSdn2hRxye+6ElpUjnjpYsfm2EjWlUdqPij4friaulKKxba/e5MnFbem46YZg/qhLKemrlQePsSgW+j3qlVK8wlB6J+1dLh6r0OdJ3e/bi+VDY8U+i4Y8xZ228N5Jkmh0kmJeIHVlQ74CvhlUE5KTQBEIEf3+mHqvcnBZLzmdm2XXsGohw6No6C4acGZ69gazmUYp0pcRp7uVOqeSvdftlmniz3hqPFsB4EMgJURmhP3IyqKCHa8ZEkqtumD50gZWM6zmULQ0orU3/TPEqU7pEz7ulk1Uqg41G+8bb4kSSWfem22fxB1ycshNM3C5qoK00sBvr7YPaD6wKAj1bk5s5c5s5hTPHGe1Fm+YGbwjAlkxsSSKe4TrEMjhUiQtbb8d5kL5DL+VcVBpZblsAsDxoW1kd0/QfHAwC8LDWrt1gN5z9JNUc3jNRY67+w8YkoSX3bmT53BCuDCWx8UfXkajHN4BbTY0m6hLUvPex3fydPSnqosn5rShdcQdNuaumPwAjvzXe4XfADPLn6SA4L794HOayP+XShYiXyxUihrSaF/RQcwXgTxh94CalkG2gvVjM9b8SUM41/Rx5xNszN+u5PF/6+FrlqB9tSWsxPjqwQ0BcBd7bd/x+zuwFbUcycQZ64q8ocnP5rEbG8rc35jdr015Ruk3nlp+Xyb48xVBWju3B3EMZGpfNQ0wGbrBt0sdVhWOw6E4SdpV8UPj+LUBMfn0w4cfW9KguIwxIPHzH6H3a0ydrUbh0W9LB51iAJJ1Ox91ahsv0lV725iNe0QzeBVA5bRv1qBujO5imtTkztUy9ffeMafIgW/yxb17f0Hig/WdAmvEnhgLvcI4aR6mZIszqXYxGASNykAPwwV5hFDJyI9MQeEHNOn3Jph2CSlJM/wDA7jvYhGRzTxWSre/mD3vpdCs6Y0LYRVQhaMu/1Vx/DkDJUjU9bnrByC2Fp/1JfWlI1bg5oXU38b6lcswjLRkkrh1p3LBs2zBnqQX4QU/CjRDKLAaA2pOglyrB6GHnHxHz8SFYpqV62DfA6riiYpvk/kUZs5FOZn4wTTAxMA0GCWCGSAFlAwQCAQUABCDC8oS25ffPw3gXEBxPZzThYA1L/9/r6ovuperHPGuC6gQUucZn0MFCLQVJLlUFvLx/8WqD7A4CAicQ";
        string keystorePass = "33all33alg";
        string keyAlias = "kitchen";
        string keyPass = "33all33alg";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
{
    // Удаляем пробелы, переносы строк и BOM
    string cleanedBase64 = keystoreBase64.Trim()
                                         .Replace("\r", "")
                                         .Replace("\n", "")
                                         .Trim('\uFEFF');

    // Создаем временный файл keystore
    tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
    File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(cleanedBase64));

    PlayerSettings.Android.useCustomKeystore = true;
    PlayerSettings.Android.keystoreName = tempKeystorePath;
    PlayerSettings.Android.keystorePass = keystorePass;
    PlayerSettings.Android.keyaliasName = keyAlias;
    PlayerSettings.Android.keyaliasPass = keyPass;

    Debug.Log("Android signing configured from Base64 keystore.");
}
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Общие параметры сборки
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Сборка AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Сборка APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Удаление временного keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}
