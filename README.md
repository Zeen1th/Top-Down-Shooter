# Top-Down Shooter Game

## GitHub Actions Workflow

We use GitHub Actions to automatically build our Unity project for Windows when pushing to the `main` branch or creating a pull request.

### How It Works:
- The workflow is triggered on any push or PR to `main`.
- It sets up Unity using the `game-ci` GitHub Actions.
- Builds the game for Windows (64-bit).
- Uploads the build as an artifact for download.

You can find the build artifacts and logs in the **Actions** tab.

---

## Branching Strategy

We use the **Git Flow** approach:

- `main`: stable and production-ready.
- `dev`: development branch where new features are merged.
- `feature/feature-name`: individual branches for each new feature or task.
- `bugfix/bug-description`: for fixing issues.

### How to Contribute:
1. Branch out from `dev`.
2. Create a `feature/` or `bugfix/` branch.
3. Make changes and push.
4. Create a pull request into `dev`.
5. After testing, merge `dev` into `main`.

