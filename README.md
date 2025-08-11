# Last Bastion: Blade & Beacon (Unity C# Scripts)

Ini adalah kumpulan skrip inti untuk memulai game Action RPG + Roguelike + Tower Defense.

Folder skrip (letakkan di `Assets/Scripts/` dalam project Unity 2D Top-Down):
- Core: `GameManager`
- Player: `PlayerController`, `Health`
- Enemies: `EnemyController`, `EnemySpawner`
- Structures: `BastionHealth`
- Combat: `Projectile`
- Towers: `TowerBase`, `ArrowTower`, `TowerSlot`, `TowerManager`
- Skills: `SkillBase`, `SkillManager`, `DashSkill`, `AoeBlastSkill`, `BuffTowerRadiusSkill`
- Upgrades: `UpgradeOption`, `UpgradeManager`
- Relics: `Relic`, `RelicManager` (stub sederhana)
- Events: `RandomEventManager` (stub sederhana)
- UI: `HUDController`, `UpgradeUI`
- Utilities: `ObjectPool` (opsional)

## Setup Cepat (Unity 2021+)
1. Buat project 2D (top-down). Import skrip ke `Assets/Scripts` sesuai struktur.
2. Siapkan Scene:
   - Buat `Empty` jadi `GameManager` lalu tambahkan komponen `GameManager` dan set referensi:
     - `bastion`: drag GameObject Base/Benteng yang punya `BastionHealth`.
     - `player`: drag Player (punya `PlayerController` + `Health`).
     - `enemySpawner`: drag object spawner (punya `EnemySpawner`) dan isi `spawnPoints`.
     - `towerManager`, `skillManager`, `upgradeManager`, `randomEventManager`, `hud`.
   - Player: GameObject 2D + `Rigidbody2D` (Dynamic), `PlayerController`, `Health`.
   - Bastion: GameObject (Static), tambahkan `BastionHealth`.
   - Spawner: GameObject + `EnemySpawner`; isi `enemyPrefab` (Enemy dengan `Rigidbody2D` + `EnemyController`).
   - UI: Setup Canvas dengan `HUDController` dan `UpgradeUI` (hubungkan ke `UpgradeManager.ui`).
   - Tower:
     - Buat prefab `ArrowTower` (GameObject dengan `ArrowTower` + child `muzzle`, isi `projectilePrefab`, `enemyMask` layer Enemy).
     - Buat `TowerSlot` di beberapa titik map dan set layer khusus lalu masukkan ke `TowerManager.slotMask`.
3. Layer & Mask: buat layer `Enemy` dan set ke musuh. Set `hitMask` pada `Projectile` ke `Enemy`.
4. Input: WASD gerak, klik kiri serang melee, Space dash, Q/E untuk skill aktif, `1` untuk mode pasang tower (klik pada slot).

## Loop Game
- Prep -> Wave -> Upgrade -> ulang ke Prep. Kalah -> Defeat (permadeath stub). Pilihan upgrade akan menambah tower/skill/buff.

## Pool Upgrade (contoh cepat)
- Buat `UpgradeOption` ScriptableObject:
  - Tower: set `type=Tower` dan isi `towerPrefab`.
  - Skill: set `type=Skill` dan isi `skill` (ScriptableObject dari `DashSkill`/`AoeBlastSkill`/`BuffTowerRadiusSkill`).
  - StatBuff: set angka buff (misal `bonusPlayerMaxHp=20`).
- Isi daftar `UpgradeManager.pool` dengan beberapa `UpgradeOption`.

## Roadmap 3 Minggu (singkat)
- Minggu 1: Movement + Melee, Spawner + Satu Tower (Arrow), Wave system dasar, HP UI.
- Minggu 2: Placement Tower Slot, Upgrade 3 opsi, Skill aktif (Q/E), sinergi tower dekat player.
- Minggu 3: Polishing VFX/SFX, balancing, menu, intro, trailer mini.

## Asset Gratis (saran)
- Tileset/Tower: Kenney Tower Defense Topdown, RPG Hero Pack (itch.io)
- SFX/Musik: Kenney Audio, Zapsplat
- Magic FX: OpenGameArt Spell FX

Catatan: Beberapa bagian (Relic, RandomEvent, UI pilihan) masih stub dan bisa dikembangkan sesuai kebutuhan. Silakan kembangkan kombinasi tower (fire/slow/lava) dengan membuat turunan `TowerBase` baru yang menambahkan efek slow/dot dll.