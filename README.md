# Infinity Block Breaker

> 벽돌 깨기 | Unity 2D, C# | 1인 | Android (Google Play)

---

## 기술 스택

- **Unity 2D, C#**
- **AdMob** — 배너·전면 광고 (2회 플레이당 전면 노출)
- **GPGS** — 로그인, 리더보드(점수·레벨), 업적 시스템

---

## 핵심 구현

| 파일 | 역할 |
|------|------|
| [Ball_Control.cs](Single/Ball_Control.cs) | 공 성장 시스템 — 터치 횟수 누적으로 데미지·외형 변화 |
| [Block_Manager.cs](Single/Block_Manager.cs) | 블록 HP·점수 레벨 연동, 색상으로 체력 시각화 |
| [Set_Making.cs](Single/Set_Making.cs) | 확률 기반 아이템 배치 — 레벨에 따라 버프/디버프 비율 역전 |
| [Game_Manager.cs](Single/Game_Manager.cs) | 게임 루프, GPGS 랭킹·업적 보고, AdMob 전면 광고 |
| [Game_Manager_Multi.cs](Multi/Game_Manager_Multi.cs) | 2인 멀티플레이 — 타이머 기반 점수 경쟁 |
| [Shop_Manager.cs](Shop/Shop_Manager.cs) | 골드·아이템·업그레이드 구매 (PlayerPrefs 저장) |

---

## 코드 구조

```
01_InfinityBlockBreaker/
├── Single/                    # 무한 모드
│   ├── Game_Manager.cs        # 게임 루프, GPGS, AdMob
│   ├── Ball_Control.cs        # 공 물리 + 성장 시스템
│   ├── Block_Manager.cs       # 블록 HP/점수/색상
│   ├── Set_Making.cs          # 블록 행 생성 + 아이템 배치 확률
│   └── Item/                  # 아이템 9종 (공 추가, 쉴드, 파워업 등)
├── Stage/                     # 스테이지 모드
├── Multi/                     # 2인 멀티플레이
│   └── Game_Manager_Multi.cs  # 타이머 기반 점수 경쟁
├── Shop/                      # 상점 (아이템 구매 + 업그레이드)
└── Object/                    # 공통 오브젝트 (바, 쉴드)
```
