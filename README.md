# Kalkulátor Tahů (Stage Rigging Calculator)

Tento projekt je moderní webová aplikace pro výpočet a vizualizaci divadelních tahů, převedená z původní Visual Basic aplikace do moderního JavaScriptu (React + TypeScript + Vite).

## 🚀 Jak aplikaci spustit

Aplikace je připravena pro hostování na **GitHub Pages**.

### Místní spuštění:
1. Ujistěte se, že máte nainstalované [Node.js](https://nodejs.org/).
2. V terminálu přejděte do složky projektu.
3. Spusťte instalaci závislostí:
   ```bash
   npm install
   ```
4. Spusťte vývojový server:
   ```bash
   npm run dev
   ```
5. Otevřete prohlížeč na adrese, kterou uvidíte v terminálu (obvykle `http://localhost:5173`).

## 🌐 Hostování na GitHubu

Tento projekt obsahuje automatizaci pro GitHub Actions. Pro zprovoznění:
1. Nahrajte tento projekt do nového GitHub repozitáře.
2. V nastavení repozitáře (**Settings > Pages**) nastavte **Source** na **GitHub Actions**.
3. Při každém `push` do větve `main` se aplikace automaticky sestaví a nasadí.

## ✨ Novinky ve webové verzi
- **Moderní UI**: Tmavý režim s průhlednými panely (Glassmorphism).
- **Interaktivní plátno**: Výběr tahů přímo kliknutím na scénu.
- **Plynulé animace**: Použití Framer Motion pro vizualizaci pohybu.
- **Režim kreslení**: Vylepšený štětec a guma pro poznámky přímo do výkresu.
- **Responzivní design**: Možnost skrýt boční panel pro maximální pracovní plochu.

## 📁 Struktura projektu
- `/src`: Zdrojové kódy aplikace.
- `/public`: Statické soubory (ikona, pozadí scény).
- `/_legacy`: Původní soubory projektu Visual Basic.

---
*Autor původní aplikace: Jiří Janík (2020)*  
*Převedeno do webové verze: Antigravity AI (2026)*
