# GitHub Copilot Instructions for Jecs Hunting with Traps (Continued)

## Mod Overview and Purpose
**Mod Name:** Jecs Hunting with Traps (Continued)  
**Author:** Jecrell  
**PackageId:** Mlie.JecsHuntingWithTraps

Jecs Hunting with Traps (Continued) is a mod for RimWorld that enhances the hunting experience by allowing traps to affect wild animals. This mod, an updated version of Jecrell's original work, integrates seamlessly with the game's existing systems and remains compatible with Tribal essentials. A potential future update may introduce bait traps for more strategic gameplay.

## Key Features and Systems
- **Wild Animal Interaction:** Traps can now injure or capture wild animals, adding another layer to hunting strategies.
- **Compatibility:** Works with Tribal essentials and designed to be minimal in conflict with other mods.
- **Future Updates:** Considerations for adding bait traps for more involved hunting tactics.

## Coding Patterns and Conventions

### C# Code
- **Naming Conventions:** Classes and methods use PascalCase, e.g., `Building_Trap_KnowsOfTrap`, while parameters and local variables use camelCase, e.g., `springChance`.
- **Class Organization:** Each class is responsible for a single aspect of the mod's functionality, adhering to single-responsibility principles.

### XML Integration
- **About File Configurations:** The `About.xml` is used for defining the mod name, author, version, and compatibility notes.
- **Zero XML Definitions:** This project does not define new XML defs but leverages existing game mechanics.

## Harmony Patching
The mod employs Harmony for patching game functions to introduce new behaviors:

- **Targeted Methods:** Patches make targeted alterations to the game's original methods without overriding them, ensuring compatibility with other mods.
- **Postfix Patch Application:** The postfix method is used to apply changes after the original methods execute, allowing augmentation of base game logic.

### Examples of Harmony Patching:
- `Building_Trap_KnowsOfTrap.Postfix`: Modifies the behavior upon trap interaction with wildlife.
- `Building_Trap_SpringChance.Postfix`: Alters the probability of trap activation upon interaction.

## Suggestions for Copilot
To streamline development and ensure consistency with existing patterns, consider the following suggestions when using GitHub Copilot:

1. **Follow Existing Naming Conventions:** Adhere to the project's established naming conventions for readability and maintainability.
   
2. **Leverage Harmony Consistently:** Use Harmony patches judiciously to ensure that the mod integrates smoothly with the base game and other mods.

3. **Structure XML Templates Clearly:** Even if XML definitions are not present, maintain clarity and organization in XML configuration files like `About.xml`.

4. **Comments and Documentation:** Encourage Copilot to generate well-documented code with comments clarifying the purpose and function of each section or method.

5. **Error Handling:** Implement robust error handling and logging to assist users and test any modifications with minimal dependencies first to isolate issues.

6. **Anticipate Future Expansions:** Write modular code to accommodate potential future features like bait traps.

By following these guidelines, leveraging Copilot can lead to productive and effective development practices for enhancing this mod further.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

