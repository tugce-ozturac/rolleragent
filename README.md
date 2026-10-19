# 🤖 RollerAgent - Unity 6.3 & ML-Agents 4.1 PPO Ortamı

Bu proje, **Unity 6.3** ve **Unity ML-Agents Release 4.1** altyapısı kullanılarak geliştirilmiş, pekiştirmeli öğrenme (Reinforcement Learning - RL) temelli özel bir yapay zeka laboratuvar ortamıdır[cite: 1, 2]. Projede, yeşil bir ajanın (`RollerAgent`) turuncu bir hedefe (`Target`) en kısa sürede ulaşmayı **PPO (Proximal Policy Optimization)** algoritmasıyla kendi kendine öğrenmesi amaçlanmıştır[cite: 1, 2, 3].

---

## 🛠️ Kullanılan Teknolojiler ve Araçlar
* **Oyun Motoru:** Unity 6.3 (Universal Render Pipeline / 3D)[cite: 1, 2]
* **Yapay Zeka Kütüphanesi:** Unity ML-Agents 4.1[cite: 1, 2]
* **Algoritma & Altyapı:** Python + PPO[cite: 1, 2]
* **Model Formatı:** ONNX (Open Neural Network Exchange)[cite: 1]

---

## 📂 Proje Mimarisi ve Klasör Yapısı
Proje, çoklu eğitim alanlarına kolayca uyarlanabilmesi için hiyerarşik bir düzende (`TrainingArea`) kurulmuştur[cite: 4, 5]:
* **`Scenes/`** -> `RollerBallTraining.unity` sahnesini barındırır[cite: 5].
* **`Scripts/`** -> Ajanın kontrol, gözlem ve ödül mekanizmalarını yöneten `RollerBallAgent.cs` dosyasını içerir[cite: 5, 7].
* **`Config/`** -> PPO hiperparametrelerinin tanımlı olduğu `rollerball_2026.yaml` dosyasını barındırır[cite: 5, 21].
* **`Models/`** -> Eğitilmiş ve otonom test için kullanılan `.onnx` model dosyalarını saklar.

---

## 🧠 Markov Karar Süreci (MDP) Tasarımı

* **Gözlemler (Observations - 8 Boyut):** 
  * Hedefin yerel konumu (`Target.localPosition` - 3 float)[cite: 3, 12]
  * Ajanın yerel konumu (`RollerAgent.localPosition` - 3 float)[cite: 3, 12]
  * Ajanın X ve Z eksenindeki hız bileşenleri (`linearVelocity` - 2 float)[cite: 3, 6, 12]
* **Eylemler (Continuous Actions):** 
  * X ve Z düzleminde Rigidbody üzerine uygulanan iki sürekli kuvvet sinyali `(-1, +1)`[cite: 3, 14].
* **Ödüller (Rewards & Episodes):** 
  * Hedefle temas: `+1` ödül ve bölümün sonlandırılması (`EndEpisode`)[cite: 3, 16].
  * Platformdan düşme: Ödül cezası ile bölümün sıfırlanması (`y < -1f`)[cite: 3, 16].

---

## 🚀 Kurulum ve Çalıştırma Adımları

### 1. Depoyu Klonlayın
```bash
git clone [https://github.com/KULLANICI_ADINIZ/rolleragent.git](https://github.com/KULLANICI_ADINIZ/rolleragent.git)
cd rolleragent
