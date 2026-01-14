---
type: spec
assembly: Sc.Core
class: AudioManager
category: Manager
status: draft
version: "1.0"
dependencies: [Singleton, ResourceManager]
created: 2025-01-14
updated: 2025-01-14
---

# AudioManager

## 역할
BGM/SFX 재생, 볼륨 조절, 페이드 효과 관리.

## 책임
- BGM/SFX 재생 및 정지
- 볼륨 계층 관리 (Master, BGM, SFX)
- BGM 전환 시 페이드 인/아웃
- 오디오 클립 캐싱

## 비책임
- 오디오 클립 생성
- 사운드 에셋 관리 (Addressables)
- 설정 저장 (SaveManager)

---

## 인터페이스

| 메서드 | 시그니처 | 설명 |
|--------|----------|------|
| PlayBGM | PlayBGM(string key, bool loop = true, float fadeTime = 0.5f) | BGM 재생 |
| StopBGM | StopBGM(float fadeTime = 0.5f) | BGM 정지 |
| PlaySFX | PlaySFX(string key, float volume = 1f) | SFX 원샷 재생 |
| SetMasterVolume | SetMasterVolume(float volume) | 마스터 볼륨 |
| SetBGMVolume | SetBGMVolume(float volume) | BGM 볼륨 |
| SetSFXVolume | SetSFXVolume(float volume) | SFX 볼륨 |
| PauseAll | PauseAll() | 전체 일시정지 |
| ResumeAll | ResumeAll() | 재개 |

---

## 볼륨 계층

```
MasterVolume (0.0 ~ 1.0)
    ├─ BGMVolume (0.0 ~ 1.0)
    │     └─ 최종 BGM = Master × BGM
    │
    └─ SFXVolume (0.0 ~ 1.0)
          └─ 최종 SFX = Master × SFX × PlayVolume
```

---

## 동작 흐름

### BGM 전환
```
PlayBGM("new_bgm")
       ↓
  현재 재생 중?
   ├─ Yes → FadeOut → Stop
   └─ No
       ↓
  ResourceManager.LoadAsync
       ↓
     클립 캐싱
       ↓
   Play + FadeIn
```

### SFX 재생
```
PlaySFX(key, volume)
       ↓
  캐시 확인
   ├─ 있음 → PlayOneShot
   └─ 없음 → LoadAsync → 캐싱 → PlayOneShot
```

---

## 사용 패턴

```csharp
// BGM 재생 (자동 페이드)
AudioManager.Instance.PlayBGM("bgm_battle");

// SFX 재생
AudioManager.Instance.PlaySFX("sfx_click", 0.8f);

// 볼륨 설정
AudioManager.Instance.SetMasterVolume(0.5f);
```

---

## 주의사항

| 항목 | 설명 |
|------|------|
| 클립 캐싱 | 자주 사용하는 SFX는 미리 로드 권장 |
| 페이드 | BGM 전환 시 자동 CrossFade |
| 볼륨 저장 | SaveManager와 연동 필요 |
| 일시정지 | BGM만 Pause, SFX는 OneShot이므로 관리 안 함 |

## 관련
- [Core.md](../Core.md)
- [Singleton.md](Singleton.md)
- [ResourceManager.md](ResourceManager.md)
