# Top-Down Shooter – CI Workflow

This Unity project uses GitHub Actions to automatically build the game for multiple platforms when changes are pushed to the repository.

## 🔧 CI Workflow Overview

The workflow is defined in `.github/workflows/unity.yml`. It runs automatically on every `push`.

### 🔁 What it does:
- **Builds the game for each supported platform:**
  - `StandaloneWindows64` (Windows)
  - `WebGL` (Web)
- **Caches the Unity `Library` folder** to speed up future builds.
- **Uses Unity Builder (game-ci)** to handle project builds.
- **Uploads each build as an artifact** so you can download and test it.

### 📁 Artifacts:
After each build, the result is available in the GitHub Actions tab under your workflow run. You’ll find downloadable builds for each platform.

## 🔐 Secrets Required

Make sure you have the following GitHub secrets set in your repo:

- `UNITY_LICENSE` – Your Unity license in text or base64 format.
- `UNITY_EMAIL` – Your Unity login email.
- `UNITY_PASSWORD` – Your Unity password or app password (if using 2FA).

Add them under **Settings > Secrets and variables > Actions**.

## 🛠️ Branching Strategy

We use a simple strategy:

- **`main`**: Stable builds. Only push or merge tested features here.
- **Feature branches**: Create branches like `feature/new-weapon` or `bugfix/enemy-ai` for each change or fix. Merge to `main` through pull requests after testing.

This keeps the main branch clean and ensures all builds passing before merging.

---

If you’re part of the team, always pull the latest `main` before starting new work and make sure your changes don’t break the build.
