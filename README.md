# KSA Localization Framework (ksa-loc-framework)

[简体中文](#简体中文) | [English](#english)

A work-in-progress localization framework for **Kitten Space Agency**, built on top of [StarMap](https://github.com/StarMapLoader). No game files are modified.

---

## 简体中文

一个还未完成的、基于 [StarMap](https://github.com/StarMapLoader) 的 Kitten Space Agency 本地化框架，无需修改任何游戏源文件。

> 注：KSA 仍然处于一个非常早期的开发阶段，很多内容和功能都较为简陋。此项目也应如是。仅作为一个尝鲜体验版使用（都怪可恶的 KSP2）。

目前处于非常早期的开发阶段，可能会随着 KSA 更新而及时更新，也可能不会。

### 功能简介

#### 1. 语言切换

在游戏的 `File -> Settings -> General` 里新增了一行 **语言 / Language** 下拉框供玩家实时切换语言。

选择后立即生效，设置会保存到 `Config/ModSettings.toml`，下次启动时会自动应用翻译。

#### 2. 自定义字体

游戏启动时会自动应用在游戏设置里选择的字体（原本游戏里是不会的），沿用原版路线，只读取 `.ttf`。

#### 3. 中文字体

- Mod 已包含宋体 `simhei.ttf`，可在设置中找到

#### 4. 硬编码文本汉化

当前游戏版本将大部分文本直接写死在代码里，这些文本不走本地化系统。

项目利用 HarmonyLib 在运行时针对各个文本出现的地方进行 Patch，将文本接入游戏自带的本地化系统，统一读 `.resx`。只 Patch UI 显示方法。

#### 5. 中文断行修复

原版 `StringTokenParser` 只按英文单词切分文本，中文长句没有空格，会整段溢出或挤在一行。框架也修复了这个问题。

### 安装

不懂的去问 AI。

1. 需要使用 StarMap，前往 [StarMap 的 GitHub](https://github.com/StarMapLoader/StarMap) 下载后解压到任意位置，然后根据页面配置游戏路径，让游戏能够从 StarMap 正常启动。

   大概操作：

   - 保证已经启动过原版游戏一次
   - 保证 `Documents\My Games\Kitten Space Agency` 下存在 `manifest.toml`
   - 保证 StarMap.exe 已启动过一次，且在同级目录下生成了 `StarMapConfig.json`
   - 修改 `StarMapConfig.json` 内的 `GameLocation` 为 KSA 的根目录

   ```json
   {
     "GameLocation": "path\\to\\Kitten Space Agency",
     "RepositoryLocation": "",
     "GameArguments": []
   }
   ```

2. 把 mod 放到以下任一位置：

   ```text
   Documents/My Games/Kitten Space Agency/mods/
   Kitten Space Agency/Content/
   ```

   两个位置选一个，推荐第一个。如果系统盘（一般指 C 盘）空间不够（怎么会够呢），可以考虑创建软链接，或者直接重定向 Documents 的路径到其他盘符。

3. 在上述位置里找到 `manifest.toml`，打开后追加下面内容即可启用：

   ```toml
   [[mods]]
   id = "ksa-loc-framework"
   enabled = true
   ```

   如果是第一次安装 Mod，进入游戏也应该会提示发现新 Mod，启动就好了。

   安装完成后用 `StarMap.exe` 启动游戏，不要直接用 `KSA.exe`。

4. 进入游戏后，在左上角菜单 `File - Settings - INTERFACE` 界面右侧先找到 Font 下拉菜单，将字体改为项目提供的新字体，点击 apply 应用；然后再找到 Language 下拉菜单切换到 `zh-CN` 即可切换语言。

### 添加或修改翻译

当前沿用游戏自带的本地化接口，除非后续游戏更新这套本地化系统。

编辑 `Languages/zh-CN.resx`：

- 保持 `<data name="...">` 与 `GameStrings` 中的字段名完全一致
- 保持 `{...}` 占位符原文不变（例如 `{Id}`、`{Vehicle}`），否则 `LString` 渲染时会抛异常
- 一段话就是一条条目，不要拆成多条拼接

新增语言：在 `Languages/` 放 `xx-XX.resx`，并在 `Languages.xml` 注册：

```xml
<LanguageFile Locale="xx-XX" Path="Languages/xx-XX.resx" />
```

再提供一个支持该语言的 `.ttf` 字体，并修改 `mod.toml` 让游戏能读取到字体路径。

要获取英文原文，在游戏中按 `\` 打开控制台，输入 `generatelanguage zh-cn` 并回车，会在 `Documents/My Games/Kitten Space Agency` 处生成翻译文件。`zh-cn` 可以换成自己的语言区域，例如 `ru-RU`。修改后放入 `Languages/`，并同步注册新语言。

### 发布

1. 本地构建：`dotnet build -c Release`（产物已自动复制到 `mods/ksa-loc-framework/`）。
2. 把产物里的 `ksa-loc-framework.dll` 复制到仓库根目录并提交。
3. 打标签推送：

   ```powershell
   git add ksa-loc-framework.dll
   git commit -m "build: v0.5.0"
   git tag v0.5.0
   git push origin master --tags
   ```

工作流 `.github/workflows/release.yml` 会在收到 `v*` 标签时自动把仓库里的 Mod 文件打成一个 zip 并创建 Release：

```text
ksa-loc-framework.zip
└── ksa-loc-framework/
    ├── ksa-loc-framework.dll
    ├── mod.toml
    ├── Languages.xml
    ├── Languages/zh-CN.resx
    ├── Patches/Localization.xml
    ├── Fonts/simhei.ttf
    └── README.md
```

> 为什么不在 CI 里编译：编译需要 KSA 的游戏程序集（`KSA.dll`、`Brutal.*.dll`）和 StarMap 的 `0Harmony.dll`，这些不能进仓库；而且 KSA 只有 Windows 版，所以 dll 由本地构建后提交。

### 已知限制

- 因为大量文本是通过一次修改 IL 实现的翻译，所以游戏后续的更新可能会破坏翻译，当然也可能不会。
- 即使这些替换在游戏更新后失效，也只会回退为英文。

### AI 辅助开发

项目开发过程中当然让 AI 参与了，不然一个人得写到地老天荒。

### 后记

AI 真是太爽了。以前做这种东西还得自己一行行翻代码，翻完还要想合适又省事的方案，方案实现完还要手写几百条本地化键，脑力活干完接着干体力活。现在我只需要设计好实现框架，剩下的全交给 AI 就好了。

其实这个本地化工程比一开始想得要麻烦。游戏里的某些文本除了硬编码之外，还存在大量 u8 字面量，dnspy 反编译时会把这类文本还原成非明文的形式，比如 `<PrivateImplementationDetails>.A9E7046282E0212EF5386D67E419F42F77E348FBEAB2086E5D82424BAD510AFA` 其实就是部件右键菜单里的 `Open Details`，它存放在 DLL 的原始字节里，直接搜是搜不出来的。

---

## English

An unfinished localization framework for **Kitten Space Agency**, built on top of [StarMap](https://github.com/StarMapLoader). It does not modify any game files.

> Note: KSA is still at a very early stage of development, and many features are rough. This project is no different — treat it as an early-access preview (blame the cursed KSP2).

This project is also very early; it may or may not keep up with KSA updates.

### Features

#### 1. Language switching

A **Language / 语言** dropdown is added under `File -> Settings -> General`, so players can switch languages at runtime.

Changes apply immediately. The selection is stored in `Config/ModSettings.toml` and re-applied on the next launch.

#### 2. Custom fonts

The font selected in the game settings is applied at startup (the vanilla game does not do this). It follows the vanilla path and only reads `.ttf` files.

#### 3. Chinese font

- The mod ships `simhei.ttf` and registers it in the game settings

#### 4. Localizing hardcoded text

Most text in the current game build is hardcoded and never goes through the localization system.

The mod uses HarmonyLib to patch the places where such text is rendered at runtime, feeding it into the game's own localization system and reading everything from `.resx` files. Only UI rendering methods are patched.

#### 5. Chinese line-wrapping fix

The vanilla `StringTokenParser` splits text on English word boundaries. Chinese sentences have no spaces, so they overflow or squeeze into a single line. The framework fixes this as well.

### Installation

If you get stuck, ask an AI.

1. You need StarMap. Download it from the [StarMap GitHub](https://github.com/StarMapLoader/StarMap), extract it anywhere, then configure the game path as described on that page so the game can be launched through StarMap.

   Roughly:

   - Launch the vanilla game at least once
   - Make sure `Documents\My Games\Kitten Space Agency\manifest.toml` exists
   - Run `StarMap.exe` once so it generates `StarMapConfig.json` next to it
   - Set `GameLocation` in `StarMapConfig.json` to your KSA install directory

   ```json
   {
     "GameLocation": "path\\to\\Kitten Space Agency",
     "RepositoryLocation": "",
     "GameArguments": []
   }
   ```

2. Put the mod into either location:

   ```text
   Documents/My Games/Kitten Space Agency/mods/
   Kitten Space Agency/Content/
   ```

   Pick one — the first is recommended. If your system drive (usually C:) is short on space, use a symlink or relocate the Documents folder to another drive.

3. Open `manifest.toml` in that directory and append:

   ```toml
   [[mods]]
   id = "ksa-loc-framework"
   enabled = true
   ```

   On a first install, the game will also prompt you about a newly discovered mod — just accept it.

   Launch the game through `StarMap.exe`, not `KSA.exe`.

4. In game, open `File - Settings - INTERFACE`, pick the bundled font from the **Font** dropdown on the right and click **Apply**, then select `zh-CN` from the **Language** dropdown.

### Adding or editing translations

The mod keeps using the game's own localization interface, unless a future game update changes that system.

Edit `Languages/zh-CN.resx`:

- Keep every `<data name="...">` identical to the corresponding field name in `GameStrings`
- Keep `{...}` placeholders untouched (e.g. `{Id}`, `{Vehicle}`) — otherwise `LString` throws while rendering
- One sentence per entry; do not split a sentence across entries

To add a language, drop `xx-XX.resx` into `Languages/` and register it in `Languages.xml`:

```xml
<LanguageFile Locale="xx-XX" Path="Languages/xx-XX.resx" />
```

You also need a `.ttf` font that covers the language, registered in `mod.toml` so the game can load it.

To obtain the English source strings, press `\` in game to open the console, run `generatelanguage zh-cn`, and a translation file is written to `Documents/My Games/Kitten Space Agency`. Replace `zh-cn` with your own locale, for example `ru-RU`. Edit the file, move it into `Languages/`, and register the new language.

### Releases

1. Build locally: `dotnet build -c Release` (the output is copied to `mods/ksa-loc-framework/` automatically).
2. Copy `ksa-loc-framework.dll` from the build output to the repository root and commit it.
3. Tag and push:

   ```powershell
   git add ksa-loc-framework.dll
   git commit -m "build: v0.5.0"
   git tag v0.5.0
   git push origin master --tags
   ```

On any `v*` tag, `.github/workflows/release.yml` zips the mod files and creates a GitHub Release:

```text
ksa-loc-framework.zip
└── ksa-loc-framework/
    ├── ksa-loc-framework.dll
    ├── mod.toml
    ├── Languages.xml
    ├── Languages/zh-CN.resx
    ├── Patches/Localization.xml
    ├── Fonts/simhei.ttf
    └── README.md
```

> Why CI does not compile the mod: building needs the game assemblies (`KSA.dll`, `Brutal.*.dll`) and StarMap's `0Harmony.dll`. Those cannot be committed, and KSA is Windows-only, so the dll is built locally and committed.

### Known limitations

- Much of the localization is done by rewriting IL once at load time, so future game updates may break the translations — or may not.
- If those rewrites stop matching after an update, the affected text simply falls back to English.

### AI-assisted development

An AI was obviously involved in building this project; doing it by hand would have taken forever.

### Afterword

Working with AI is a blast. It used to mean reading decompiled code line by line, then figuring out a cheap approach, then hand-writing hundreds of localization keys — mental work followed by manual labor. Now I just design the framework and let the AI handle the rest.

This localization job turned out to be trickier than expected. Besides hardcoded strings, the game contains a lot of `u8` literals that decompilers render as opaque blobs: `<PrivateImplementationDetails>.A9E7046282E0212EF5386D67E419F42F77E348FBEAB2086E5D82424BAD510AFA` is really `Open Details` in the part context menu, stored as raw bytes inside the DLL and impossible to find with a plain text search.
