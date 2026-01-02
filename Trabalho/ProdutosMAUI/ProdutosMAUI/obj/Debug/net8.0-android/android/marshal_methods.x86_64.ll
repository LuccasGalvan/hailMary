; ModuleID = 'marshal_methods.x86_64.ll'
source_filename = "marshal_methods.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [373 x ptr] zeroinitializer, align 16

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [746 x i64] [
	i64 24362543149721218, ; 0: Xamarin.AndroidX.DynamicAnimation => 0x568d9a9a43a682 => 279
	i64 98382396393917666, ; 1: Microsoft.Extensions.Primitives.dll => 0x15d8644ad360ce2 => 230
	i64 120698629574877762, ; 2: Mono.Android => 0x1accec39cafe242 => 171
	i64 131669012237370309, ; 3: Microsoft.Maui.Essentials.dll => 0x1d3c844de55c3c5 => 238
	i64 160518225272466977, ; 4: Microsoft.Extensions.Hosting.Abstractions => 0x23a4679b5576e21 => 223
	i64 196720943101637631, ; 5: System.Linq.Expressions.dll => 0x2bae4a7cd73f3ff => 58
	i64 210515253464952879, ; 6: Xamarin.AndroidX.Collection.dll => 0x2ebe681f694702f => 266
	i64 229794953483747371, ; 7: System.ValueTuple.dll => 0x330654aed93802b => 151
	i64 232391251801502327, ; 8: Xamarin.AndroidX.SavedState.dll => 0x3399e9cbc897277 => 307
	i64 295915112840604065, ; 9: Xamarin.AndroidX.SlidingPaneLayout => 0x41b4d3a3088a9a1 => 310
	i64 316157742385208084, ; 10: Xamarin.AndroidX.Core.Core.Ktx.dll => 0x46337caa7dc1b14 => 273
	i64 350667413455104241, ; 11: System.ServiceProcess.dll => 0x4ddd227954be8f1 => 132
	i64 422779754995088667, ; 12: System.IO.UnmanagedMemoryStream => 0x5de03f27ab57d1b => 56
	i64 435118502366263740, ; 13: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x609d9f8f8bdb9bc => 309
	i64 535107122908063503, ; 14: Microsoft.Extensions.ObjectPool.dll => 0x76d1517d9b7670f => 228
	i64 545109961164950392, ; 15: fi/Microsoft.Maui.Controls.resources.dll => 0x7909e9f1ec38b78 => 342
	i64 560278790331054453, ; 16: System.Reflection.Primitives => 0x7c6829760de3975 => 95
	i64 600385053610559562, ; 17: RCLComum.dll => 0x854ff0678e9484a => 370
	i64 634308326490598313, ; 18: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x8cd840fee8b6ba9 => 292
	i64 648449422406355874, ; 19: Microsoft.Extensions.Configuration.FileExtensions.dll => 0x8ffc15065568ba2 => 214
	i64 649145001856603771, ; 20: System.Security.SecureString => 0x90239f09b62167b => 129
	i64 668723562677762733, ; 21: Microsoft.Extensions.Configuration.Binder.dll => 0x947c88986577aad => 213
	i64 683390398661839228, ; 22: Microsoft.Extensions.FileProviders.Embedded => 0x97be3f26326c97c => 220
	i64 750875890346172408, ; 23: System.Threading.Thread => 0xa6ba5a4da7d1ff8 => 145
	i64 798450721097591769, ; 24: Xamarin.AndroidX.Collection.Ktx.dll => 0xb14aab351ad2bd9 => 267
	i64 799765834175365804, ; 25: System.ComponentModel.dll => 0xb1956c9f18442ac => 18
	i64 849051935479314978, ; 26: hi/Microsoft.Maui.Controls.resources.dll => 0xbc8703ca21a3a22 => 345
	i64 870603111519317375, ; 27: SQLitePCLRaw.lib.e_sqlite3.android => 0xc1500ead2756d7f => 244
	i64 872800313462103108, ; 28: Xamarin.AndroidX.DrawerLayout => 0xc1ccf42c3c21c44 => 278
	i64 895210737996778430, ; 29: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0xc6c6d6c5569cbbe => 293
	i64 940822596282819491, ; 30: System.Transactions => 0xd0e792aa81923a3 => 150
	i64 960778385402502048, ; 31: System.Runtime.Handles.dll => 0xd555ed9e1ca1ba0 => 104
	i64 1001381392624924420, ; 32: Microsoft.AspNetCore.Authentication.Core.dll => 0xde59f1230183704 => 183
	i64 1010599046655515943, ; 33: System.Reflection.Primitives.dll => 0xe065e7a82401d27 => 95
	i64 1120440138749646132, ; 34: Xamarin.Google.Android.Material.dll => 0xf8c9a5eae431534 => 323
	i64 1121665720830085036, ; 35: nb/Microsoft.Maui.Controls.resources.dll => 0xf90f507becf47ac => 353
	i64 1152743041985833843, ; 36: RCLProdutos.dll => 0xfff5db06ede6b73 => 371
	i64 1190479578843326660, ; 37: Microsoft.AspNetCore.Components.QuickGrid.EntityFrameworkAdapter => 0x10856ede995028c4 => 189
	i64 1268860745194512059, ; 38: System.Drawing.dll => 0x119be62002c19ebb => 36
	i64 1301485588176585670, ; 39: SQLitePCLRaw.core => 0x120fce3f338e43c6 => 243
	i64 1301626418029409250, ; 40: System.Diagnostics.FileVersionInfo => 0x12104e54b4e833e2 => 28
	i64 1315114680217950157, ; 41: Xamarin.AndroidX.Arch.Core.Common.dll => 0x124039d5794ad7cd => 262
	i64 1326794923564391531, ; 42: Microsoft.AspNetCore.Authentication => 0x1269b8f40cd8dc6b => 180
	i64 1369545283391376210, ; 43: Xamarin.AndroidX.Navigation.Fragment.dll => 0x13019a2dd85acb52 => 300
	i64 1404195534211153682, ; 44: System.IO.FileSystem.Watcher.dll => 0x137cb4660bd87f12 => 50
	i64 1425944114962822056, ; 45: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 115
	i64 1476839205573959279, ; 46: System.Net.Primitives.dll => 0x147ec96ece9b1e6f => 70
	i64 1486715745332614827, ; 47: Microsoft.Maui.Controls.dll => 0x14a1e017ea87d6ab => 235
	i64 1492954217099365037, ; 48: System.Net.HttpListener => 0x14b809f350210aad => 65
	i64 1513467482682125403, ; 49: Mono.Android.Runtime => 0x1500eaa8245f6c5b => 170
	i64 1515118852840071656, ; 50: Microsoft.AspNet.Identity.EntityFramework.dll => 0x1506c891b807a1e8 => 179
	i64 1518315023656898250, ; 51: SQLitePCLRaw.provider.e_sqlite3 => 0x151223783a354eca => 245
	i64 1537168428375924959, ; 52: System.Threading.Thread.dll => 0x15551e8a954ae0df => 145
	i64 1556147632182429976, ; 53: ko/Microsoft.Maui.Controls.resources.dll => 0x15988c06d24c8918 => 351
	i64 1576750169145655260, ; 54: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x15e1bdecc376bfdc => 321
	i64 1624659445732251991, ; 55: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0x168bf32877da9957 => 261
	i64 1628611045998245443, ; 56: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0x1699fd1e1a00b643 => 296
	i64 1636321030536304333, ; 57: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0x16b5614ec39e16cd => 286
	i64 1639340239664632727, ; 58: Microsoft.AspNetCore.Cryptography.Internal.dll => 0x16c01b432b36d397 => 194
	i64 1651782184287836205, ; 59: System.Globalization.Calendars => 0x16ec4f2524cb982d => 40
	i64 1659332977923810219, ; 60: System.Reflection.DispatchProxy => 0x1707228d493d63ab => 89
	i64 1682513316613008342, ; 61: System.Net.dll => 0x17597cf276952bd6 => 81
	i64 1735388228521408345, ; 62: System.Net.Mail.dll => 0x181556663c69b759 => 66
	i64 1743969030606105336, ; 63: System.Memory.dll => 0x1833d297e88f2af8 => 62
	i64 1767386781656293639, ; 64: System.Private.Uri.dll => 0x188704e9f5582107 => 86
	i64 1776954265264967804, ; 65: Microsoft.JSInterop.dll => 0x18a9027d533bd07c => 232
	i64 1795316252682057001, ; 66: Xamarin.AndroidX.AppCompat.dll => 0x18ea3e9eac997529 => 260
	i64 1825687700144851180, ; 67: System.Runtime.InteropServices.RuntimeInformation.dll => 0x1956254a55ef08ec => 106
	i64 1835311033149317475, ; 68: es\Microsoft.Maui.Controls.resources => 0x197855a927386163 => 341
	i64 1836611346387731153, ; 69: Xamarin.AndroidX.SavedState => 0x197cf449ebe482d1 => 307
	i64 1847446322536158010, ; 70: DocumentFormat.OpenXml.Framework.dll => 0x19a372a4645e933a => 174
	i64 1854145951182283680, ; 71: System.Runtime.CompilerServices.VisualC => 0x19bb3feb3df2e3a0 => 102
	i64 1875417405349196092, ; 72: System.Drawing.Primitives => 0x1a06d2319b6c713c => 35
	i64 1875917498431009007, ; 73: Xamarin.AndroidX.Annotation.dll => 0x1a08990699eb70ef => 257
	i64 1881198190668717030, ; 74: tr\Microsoft.Maui.Controls.resources => 0x1a1b5bc992ea9be6 => 363
	i64 1897575647115118287, ; 75: Xamarin.AndroidX.Security.SecurityCrypto => 0x1a558aff4cba86cf => 309
	i64 1920760634179481754, ; 76: Microsoft.Maui.Controls.Xaml => 0x1aa7e99ec2d2709a => 236
	i64 1959996714666907089, ; 77: tr/Microsoft.Maui.Controls.resources.dll => 0x1b334ea0a2a755d1 => 363
	i64 1972385128188460614, ; 78: System.Security.Cryptography.Algorithms => 0x1b5f51d2edefbe46 => 119
	i64 1981742497975770890, ; 79: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x1b80904d5c241f0a => 294
	i64 1983698669889758782, ; 80: cs/Microsoft.Maui.Controls.resources.dll => 0x1b87836e2031a63e => 337
	i64 2019660174692588140, ; 81: pl/Microsoft.Maui.Controls.resources.dll => 0x1c07463a6f8e1a6c => 355
	i64 2040001226662520565, ; 82: System.Threading.Tasks.Extensions.dll => 0x1c4f8a4ea894a6f5 => 142
	i64 2062890601515140263, ; 83: System.Threading.Tasks.Dataflow => 0x1ca0dc1289cd44a7 => 141
	i64 2064708342624596306, ; 84: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x1ca7514c5eecb152 => 331
	i64 2080945842184875448, ; 85: System.IO.MemoryMappedFiles => 0x1ce10137d8416db8 => 53
	i64 2102659300918482391, ; 86: System.Drawing.Primitives.dll => 0x1d2e257e6aead5d7 => 35
	i64 2106033277907880740, ; 87: System.Threading.Tasks.Dataflow.dll => 0x1d3a221ba6d9cb24 => 141
	i64 2165310824878145998, ; 88: Xamarin.Android.Glide.GifDecoder => 0x1e0cbab9112b81ce => 254
	i64 2165725771938924357, ; 89: Xamarin.AndroidX.Browser => 0x1e0e341d75540745 => 264
	i64 2192948757939169934, ; 90: Microsoft.EntityFrameworkCore.Abstractions.dll => 0x1e6eeb46cf992a8e => 208
	i64 2200176636225660136, ; 91: Microsoft.Extensions.Logging.Debug.dll => 0x1e8898fe5d5824e8 => 227
	i64 2207662933261301575, ; 92: DocumentFormat.OpenXml => 0x1ea331bdb8d63747 => 173
	i64 2262844636196693701, ; 93: Xamarin.AndroidX.DrawerLayout.dll => 0x1f673d352266e6c5 => 278
	i64 2287834202362508563, ; 94: System.Collections.Concurrent => 0x1fc00515e8ce7513 => 8
	i64 2287887973817120656, ; 95: System.ComponentModel.DataAnnotations.dll => 0x1fc035fd8d41f790 => 14
	i64 2295368378960711535, ; 96: Microsoft.AspNetCore.Components.WebView.Maui.dll => 0x1fdac961189e0f6f => 193
	i64 2302323944321350744, ; 97: ru/Microsoft.Maui.Controls.resources.dll => 0x1ff37f6ddb267c58 => 359
	i64 2304837677853103545, ; 98: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0x1ffc6da80d5ed5b9 => 306
	i64 2315304989185124968, ; 99: System.IO.FileSystem.dll => 0x20219d9ee311aa68 => 51
	i64 2329709569556905518, ; 100: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x2054ca829b447e2e => 289
	i64 2335503487726329082, ; 101: System.Text.Encodings.Web => 0x2069600c4d9d1cfa => 136
	i64 2337758774805907496, ; 102: System.Runtime.CompilerServices.Unsafe => 0x207163383edbc828 => 101
	i64 2470498323731680442, ; 103: Xamarin.AndroidX.CoordinatorLayout => 0x2248f922dc398cba => 271
	i64 2479423007379663237, ; 104: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x2268ae16b2cba985 => 316
	i64 2497223385847772520, ; 105: System.Runtime => 0x22a7eb7046413568 => 116
	i64 2547086958574651984, ; 106: Xamarin.AndroidX.Activity.dll => 0x2359121801df4a50 => 255
	i64 2592350477072141967, ; 107: System.Xml.dll => 0x23f9e10627330e8f => 163
	i64 2602673633151553063, ; 108: th\Microsoft.Maui.Controls.resources => 0x241e8de13a460e27 => 362
	i64 2624866290265602282, ; 109: mscorlib.dll => 0x246d65fbde2db8ea => 166
	i64 2632269733008246987, ; 110: System.Net.NameResolution => 0x2487b36034f808cb => 67
	i64 2656907746661064104, ; 111: Microsoft.Extensions.DependencyInjection => 0x24df3b84c8b75da8 => 216
	i64 2662981627730767622, ; 112: cs\Microsoft.Maui.Controls.resources => 0x24f4cfae6c48af06 => 337
	i64 2706075432581334785, ; 113: System.Net.WebSockets => 0x258de944be6c0701 => 80
	i64 2741613924229396860, ; 114: EntityFramework.SqlServer.dll => 0x260c2b56a0f5397c => 175
	i64 2781169761569919449, ; 115: Microsoft.JSInterop => 0x2698b329b26ed1d9 => 232
	i64 2783046991838674048, ; 116: System.Runtime.CompilerServices.Unsafe.dll => 0x269f5e7e6dc37c80 => 101
	i64 2787234703088983483, ; 117: Xamarin.AndroidX.Startup.StartupRuntime => 0x26ae3f31ef429dbb => 311
	i64 2815524396660695947, ; 118: System.Security.AccessControl => 0x2712c0857f68238b => 117
	i64 2895129759130297543, ; 119: fi\Microsoft.Maui.Controls.resources => 0x282d912d479fa4c7 => 342
	i64 2923871038697555247, ; 120: Jsr305Binding => 0x2893ad37e69ec52f => 324
	i64 3017136373564924869, ; 121: System.Net.WebProxy => 0x29df058bd93f63c5 => 78
	i64 3017704767998173186, ; 122: Xamarin.Google.Android.Material => 0x29e10a7f7d88a002 => 323
	i64 3080904423057169497, ; 123: RCLAPI => 0x2ac1923fdb409c59 => 369
	i64 3106852385031680087, ; 124: System.Runtime.Serialization.Xml => 0x2b1dc1c88b637057 => 114
	i64 3110390492489056344, ; 125: System.Security.Cryptography.Csp.dll => 0x2b2a53ac61900058 => 121
	i64 3135773902340015556, ; 126: System.IO.FileSystem.DriveInfo.dll => 0x2b8481c008eac5c4 => 48
	i64 3168817962471953758, ; 127: Microsoft.Extensions.Hosting.Abstractions.dll => 0x2bf9e725d304955e => 223
	i64 3266690593535380875, ; 128: Microsoft.AspNetCore.Authorization => 0x2d559dc982c94d8b => 184
	i64 3281594302220646930, ; 129: System.Security.Principal => 0x2d8a90a198ceba12 => 128
	i64 3289520064315143713, ; 130: Xamarin.AndroidX.Lifecycle.Common => 0x2da6b911e3063621 => 287
	i64 3303437397778967116, ; 131: Xamarin.AndroidX.Annotation.Experimental => 0x2dd82acf985b2a4c => 258
	i64 3311221304742556517, ; 132: System.Numerics.Vectors.dll => 0x2df3d23ba9e2b365 => 82
	i64 3325875462027654285, ; 133: System.Runtime.Numerics => 0x2e27e21c8958b48d => 110
	i64 3328853167529574890, ; 134: System.Net.Sockets.dll => 0x2e327651a008c1ea => 75
	i64 3344514922410554693, ; 135: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x2e6a1a9a18463545 => 334
	i64 3396143930648122816, ; 136: Microsoft.Extensions.FileProviders.Abstractions => 0x2f2186e9506155c0 => 218
	i64 3429672777697402584, ; 137: Microsoft.Maui.Essentials => 0x2f98a5385a7b1ed8 => 238
	i64 3437845325506641314, ; 138: System.IO.MemoryMappedFiles.dll => 0x2fb5ae1beb8f7da2 => 53
	i64 3493805808809882663, ; 139: Xamarin.AndroidX.Tracing.Tracing.dll => 0x307c7ddf444f3427 => 313
	i64 3494946837667399002, ; 140: Microsoft.Extensions.Configuration => 0x30808ba1c00a455a => 211
	i64 3508450208084372758, ; 141: System.Net.Ping => 0x30b084e02d03ad16 => 69
	i64 3522470458906976663, ; 142: Xamarin.AndroidX.SwipeRefreshLayout => 0x30e2543832f52197 => 312
	i64 3523004241079211829, ; 143: Microsoft.Extensions.Caching.Memory.dll => 0x30e439b10bb89735 => 210
	i64 3531994851595924923, ; 144: System.Numerics => 0x31042a9aade235bb => 83
	i64 3551103847008531295, ; 145: System.Private.CoreLib.dll => 0x31480e226177735f => 172
	i64 3567343442040498961, ; 146: pt\Microsoft.Maui.Controls.resources => 0x3181bff5bea4ab11 => 357
	i64 3571415421602489686, ; 147: System.Runtime.dll => 0x319037675df7e556 => 116
	i64 3638003163729360188, ; 148: Microsoft.Extensions.Configuration.Abstractions => 0x327cc89a39d5f53c => 212
	i64 3647754201059316852, ; 149: System.Xml.ReaderWriter => 0x329f6d1e86145474 => 156
	i64 3655542548057982301, ; 150: Microsoft.Extensions.Configuration.dll => 0x32bb18945e52855d => 211
	i64 3659371656528649588, ; 151: Xamarin.Android.Glide.Annotations => 0x32c8b3222885dd74 => 252
	i64 3716579019761409177, ; 152: netstandard.dll => 0x3393f0ed5c8c5c99 => 167
	i64 3727469159507183293, ; 153: Xamarin.AndroidX.RecyclerView => 0x33baa1739ba646bd => 305
	i64 3753897248517198740, ; 154: Microsoft.AspNetCore.Components.WebView.dll => 0x341885a8952f1394 => 192
	i64 3772598417116884899, ; 155: Xamarin.AndroidX.DynamicAnimation.dll => 0x345af645b473efa3 => 279
	i64 3794322307918838949, ; 156: Microsoft.AspNetCore.Metadata.dll => 0x34a824092ed7bca5 => 205
	i64 3869221888984012293, ; 157: Microsoft.Extensions.Logging.dll => 0x35b23cceda0ed605 => 225
	i64 3869649043256705283, ; 158: System.Diagnostics.Tools => 0x35b3c14d74bf0103 => 32
	i64 3889433610606858880, ; 159: Microsoft.Extensions.FileProviders.Physical.dll => 0x35fa0b4301afd280 => 221
	i64 3890352374528606784, ; 160: Microsoft.Maui.Controls.Xaml.dll => 0x35fd4edf66e00240 => 236
	i64 3919223565570527920, ; 161: System.Security.Cryptography.Encoding => 0x3663e111652bd2b0 => 122
	i64 3933965368022646939, ; 162: System.Net.Requests => 0x369840a8bfadc09b => 72
	i64 3966267475168208030, ; 163: System.Memory => 0x370b03412596249e => 62
	i64 4005135229510678782, ; 164: Microsoft.AspNetCore.DataProtection.Abstractions => 0x379519456862f8fe => 197
	i64 4006972109285359177, ; 165: System.Xml.XmlDocument => 0x379b9fe74ed9fe49 => 161
	i64 4009997192427317104, ; 166: System.Runtime.Serialization.Primitives => 0x37a65f335cf1a770 => 113
	i64 4073500526318903918, ; 167: System.Private.Xml.dll => 0x3887fb25779ae26e => 88
	i64 4073631083018132676, ; 168: Microsoft.Maui.Controls.Compatibility.dll => 0x388871e311491cc4 => 234
	i64 4120493066591692148, ; 169: zh-Hant\Microsoft.Maui.Controls.resources => 0x392eee9cdda86574 => 368
	i64 4148881117810174540, ; 170: System.Runtime.InteropServices.JavaScript.dll => 0x3993c9651a66aa4c => 105
	i64 4154383907710350974, ; 171: System.ComponentModel => 0x39a7562737acb67e => 18
	i64 4167269041631776580, ; 172: System.Threading.ThreadPool => 0x39d51d1d3df1cf44 => 146
	i64 4168469861834746866, ; 173: System.Security.Claims.dll => 0x39d96140fb94ebf2 => 118
	i64 4187479170553454871, ; 174: System.Linq.Expressions => 0x3a1cea1e912fa117 => 58
	i64 4201423742386704971, ; 175: Xamarin.AndroidX.Core.Core.Ktx => 0x3a4e74a233da124b => 273
	i64 4205801962323029395, ; 176: System.ComponentModel.TypeConverter => 0x3a5e0299f7e7ad93 => 17
	i64 4206372723267232883, ; 177: RCLAPI.dll => 0x3a6009b49b3c6473 => 369
	i64 4225924121207573736, ; 178: Microsoft.AspNetCore.Authentication.Abstractions => 0x3aa57f992c550ce8 => 181
	i64 4235503420553921860, ; 179: System.IO.IsolatedStorage.dll => 0x3ac787eb9b118544 => 52
	i64 4243591448627561453, ; 180: Microsoft.AspNetCore.Http.Extensions.dll => 0x3ae443f06354c3ed => 202
	i64 4250192876909962317, ; 181: Microsoft.AspNetCore.Hosting.Abstractions => 0x3afbb7e72f1d244d => 198
	i64 4282138915307457788, ; 182: System.Reflection.Emit => 0x3b6d36a7ddc70cfc => 92
	i64 4337444564132831293, ; 183: SQLitePCLRaw.batteries_v2.dll => 0x3c31b2d9ae16203d => 242
	i64 4356591372459378815, ; 184: vi/Microsoft.Maui.Controls.resources.dll => 0x3c75b8c562f9087f => 365
	i64 4373617458794931033, ; 185: System.IO.Pipes.dll => 0x3cb235e806eb2359 => 55
	i64 4376666294362535240, ; 186: RCLComum => 0x3cbd0ace5fe77548 => 370
	i64 4384840217421645357, ; 187: Microsoft.AspNetCore.Components.Forms => 0x3cda14f22443862d => 187
	i64 4397634830160618470, ; 188: System.Security.SecureString.dll => 0x3d0789940f9be3e6 => 129
	i64 4477672992252076438, ; 189: System.Web.HttpUtility.dll => 0x3e23e3dcdb8ba196 => 152
	i64 4484706122338676047, ; 190: System.Globalization.Extensions.dll => 0x3e3ce07510042d4f => 41
	i64 4533124835995628778, ; 191: System.Reflection.Emit.dll => 0x3ee8e505540534ea => 92
	i64 4612482779465751747, ; 192: Microsoft.EntityFrameworkCore.Abstractions => 0x4002d4a662a99cc3 => 208
	i64 4636684751163556186, ; 193: Xamarin.AndroidX.VersionedParcelable.dll => 0x4058d0370893015a => 317
	i64 4672453897036726049, ; 194: System.IO.FileSystem.Watcher => 0x40d7e4104a437f21 => 50
	i64 4679594760078841447, ; 195: ar/Microsoft.Maui.Controls.resources.dll => 0x40f142a407475667 => 335
	i64 4716677666592453464, ; 196: System.Xml.XmlSerializer => 0x417501590542f358 => 162
	i64 4743821336939966868, ; 197: System.ComponentModel.Annotations => 0x41d5705f4239b194 => 13
	i64 4759461199762736555, ; 198: Xamarin.AndroidX.Lifecycle.Process.dll => 0x420d00be961cc5ab => 291
	i64 4794310189461587505, ; 199: Xamarin.AndroidX.Activity => 0x4288cfb749e4c631 => 255
	i64 4795410492532947900, ; 200: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0x428cb86f8f9b7bbc => 312
	i64 4809057822547766521, ; 201: System.Drawing => 0x42bd349c3145ecf9 => 36
	i64 4814660307502931973, ; 202: System.Net.NameResolution.dll => 0x42d11c0a5ee2a005 => 67
	i64 4853321196694829351, ; 203: System.Runtime.Loader.dll => 0x435a75ea15de7927 => 109
	i64 5055365687667823624, ; 204: Xamarin.AndroidX.Activity.Ktx.dll => 0x4628444ef7239408 => 256
	i64 5081566143765835342, ; 205: System.Resources.ResourceManager.dll => 0x4685597c05d06e4e => 99
	i64 5099468265966638712, ; 206: System.Resources.ResourceManager => 0x46c4f35ea8519678 => 99
	i64 5103417709280584325, ; 207: System.Collections.Specialized => 0x46d2fb5e161b6285 => 11
	i64 5106322746114322454, ; 208: Microsoft.AspNetCore.Authentication.dll => 0x46dd4d7baea43016 => 180
	i64 5112836352847824253, ; 209: Microsoft.AspNetCore.WebUtilities.dll => 0x46f47192ee32c57d => 206
	i64 5119492727527269518, ; 210: Microsoft.AspNet.Identity.Core.dll => 0x470c1782ee75648e => 178
	i64 5177565741364132164, ; 211: Microsoft.AspNetCore.Http => 0x47da689c1f3db944 => 200
	i64 5182934613077526976, ; 212: System.Collections.Specialized.dll => 0x47ed7b91fa9009c0 => 11
	i64 5197073077358930460, ; 213: Microsoft.AspNetCore.Components.Web.dll => 0x481fb66db7b9aa1c => 190
	i64 5203618020066742981, ; 214: Xamarin.Essentials => 0x4836f704f0e652c5 => 322
	i64 5205316157927637098, ; 215: Xamarin.AndroidX.LocalBroadcastManager => 0x483cff7778e0c06a => 298
	i64 5244375036463807528, ; 216: System.Diagnostics.Contracts.dll => 0x48c7c34f4d59fc28 => 25
	i64 5262971552273843408, ; 217: System.Security.Principal.dll => 0x4909d4be0c44c4d0 => 128
	i64 5278787618751394462, ; 218: System.Net.WebClient.dll => 0x4942055efc68329e => 76
	i64 5280980186044710147, ; 219: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x4949cf7fd7123d03 => 290
	i64 5290786973231294105, ; 220: System.Runtime.Loader => 0x496ca6b869b72699 => 109
	i64 5376510917114486089, ; 221: Xamarin.AndroidX.VectorDrawable.Animated => 0x4a9d3431719e5d49 => 316
	i64 5408338804355907810, ; 222: Xamarin.AndroidX.Transition => 0x4b0e477cea9840e2 => 314
	i64 5423376490970181369, ; 223: System.Runtime.InteropServices.RuntimeInformation => 0x4b43b42f2b7b6ef9 => 106
	i64 5440320908473006344, ; 224: Microsoft.VisualBasic.Core => 0x4b7fe70acda9f908 => 2
	i64 5446034149219586269, ; 225: System.Diagnostics.Debug => 0x4b94333452e150dd => 26
	i64 5451019430259338467, ; 226: Xamarin.AndroidX.ConstraintLayout.dll => 0x4ba5e94a845c2ce3 => 269
	i64 5457765010617926378, ; 227: System.Xml.Serialization => 0x4bbde05c557002ea => 157
	i64 5471532531798518949, ; 228: sv\Microsoft.Maui.Controls.resources => 0x4beec9d926d82ca5 => 361
	i64 5507995362134886206, ; 229: System.Core.dll => 0x4c705499688c873e => 21
	i64 5522859530602327440, ; 230: uk\Microsoft.Maui.Controls.resources => 0x4ca5237b51eead90 => 364
	i64 5527431512186326818, ; 231: System.IO.FileSystem.Primitives.dll => 0x4cb561acbc2a8f22 => 49
	i64 5570799893513421663, ; 232: System.IO.Compression.Brotli => 0x4d4f74fcdfa6c35f => 43
	i64 5573260873512690141, ; 233: System.Security.Cryptography.dll => 0x4d58333c6e4ea1dd => 126
	i64 5574231584441077149, ; 234: Xamarin.AndroidX.Annotation.Jvm => 0x4d5ba617ae5f8d9d => 259
	i64 5591791169662171124, ; 235: System.Linq.Parallel => 0x4d9a087135e137f4 => 59
	i64 5610815111739789596, ; 236: Microsoft.AspNetCore.Authentication.Core => 0x4ddd9e9de3a4d11c => 183
	i64 5650097808083101034, ; 237: System.Security.Cryptography.Algorithms.dll => 0x4e692e055d01a56a => 119
	i64 5692067934154308417, ; 238: Xamarin.AndroidX.ViewPager2.dll => 0x4efe49a0d4a8bb41 => 319
	i64 5724799082821825042, ; 239: Xamarin.AndroidX.ExifInterface => 0x4f72926f3e13b212 => 282
	i64 5757522595884336624, ; 240: Xamarin.AndroidX.Concurrent.Futures.dll => 0x4fe6d44bd9f885f0 => 268
	i64 5783556987928984683, ; 241: Microsoft.VisualBasic => 0x504352701bbc3c6b => 3
	i64 5896680224035167651, ; 242: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x51d5376bfbafdda3 => 288
	i64 5918659295201451090, ; 243: Microsoft.AspNetCore.Components.WebAssembly => 0x52234d45450bdc52 => 191
	i64 5959344983920014087, ; 244: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x52b3d8b05c8ef307 => 308
	i64 5979151488806146654, ; 245: System.Formats.Asn1 => 0x52fa3699a489d25e => 38
	i64 5984759512290286505, ; 246: System.Security.Cryptography.Primitives => 0x530e23115c33dba9 => 124
	i64 6068057819846744445, ; 247: ro/Microsoft.Maui.Controls.resources.dll => 0x5436126fec7f197d => 358
	i64 6102788177522843259, ; 248: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0x54b1758374b3de7b => 308
	i64 6165777499998190031, ; 249: ProdutosMAUI => 0x55913df7f37cf9cf => 0
	i64 6182525717148725503, ; 250: Microsoft.AspNetCore.Components.Authorization => 0x55ccbe62215df0ff => 186
	i64 6183170893902868313, ; 251: SQLitePCLRaw.batteries_v2 => 0x55cf092b0c9d6f59 => 242
	i64 6200764641006662125, ; 252: ro\Microsoft.Maui.Controls.resources => 0x560d8a96830131ed => 358
	i64 6222399776351216807, ; 253: System.Text.Json.dll => 0x565a67a0ffe264a7 => 137
	i64 6251069312384999852, ; 254: System.Transactions.Local => 0x56c0426b870da1ac => 149
	i64 6270168066149956752, ; 255: RCLProdutos => 0x57041ca2a8ddbc90 => 371
	i64 6278736998281604212, ; 256: System.Private.DataContractSerialization => 0x57228e08a4ad6c74 => 85
	i64 6284145129771520194, ; 257: System.Reflection.Emit.ILGeneration => 0x5735c4b3610850c2 => 90
	i64 6319713645133255417, ; 258: Xamarin.AndroidX.Lifecycle.Runtime => 0x57b42213b45b52f9 => 292
	i64 6357457916754632952, ; 259: _Microsoft.Android.Resource.Designer => 0x583a3a4ac2a7a0f8 => 372
	i64 6401687960814735282, ; 260: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0x58d75d486341cfb2 => 289
	i64 6478287442656530074, ; 261: hr\Microsoft.Maui.Controls.resources => 0x59e7801b0c6a8e9a => 346
	i64 6504860066809920875, ; 262: Xamarin.AndroidX.Browser.dll => 0x5a45e7c43bd43d6b => 264
	i64 6548213210057960872, ; 263: Xamarin.AndroidX.CustomView.dll => 0x5adfed387b066da8 => 275
	i64 6557084851308642443, ; 264: Xamarin.AndroidX.Window.dll => 0x5aff71ee6c58c08b => 320
	i64 6560151584539558821, ; 265: Microsoft.Extensions.Options => 0x5b0a571be53243a5 => 229
	i64 6589202984700901502, ; 266: Xamarin.Google.ErrorProne.Annotations.dll => 0x5b718d34180a787e => 326
	i64 6591971792923354531, ; 267: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x5b7b636b7e9765a3 => 290
	i64 6617685658146568858, ; 268: System.Text.Encoding.CodePages => 0x5bd6be0b4905fa9a => 133
	i64 6713440830605852118, ; 269: System.Reflection.TypeExtensions.dll => 0x5d2aeeddb8dd7dd6 => 96
	i64 6739853162153639747, ; 270: Microsoft.VisualBasic.dll => 0x5d88c4bde075ff43 => 3
	i64 6743165466166707109, ; 271: nl\Microsoft.Maui.Controls.resources => 0x5d948943c08c43a5 => 354
	i64 6772837112740759457, ; 272: System.Runtime.InteropServices.JavaScript => 0x5dfdf378527ec7a1 => 105
	i64 6777482997383978746, ; 273: pt/Microsoft.Maui.Controls.resources.dll => 0x5e0e74e0a2525efa => 357
	i64 6786606130239981554, ; 274: System.Diagnostics.TraceSource => 0x5e2ede51877147f2 => 33
	i64 6798329586179154312, ; 275: System.Windows => 0x5e5884bd523ca188 => 154
	i64 6814185388980153342, ; 276: System.Xml.XDocument.dll => 0x5e90d98217d1abfe => 158
	i64 6876862101832370452, ; 277: System.Xml.Linq => 0x5f6f85a57d108914 => 155
	i64 6894844156784520562, ; 278: System.Numerics.Vectors => 0x5faf683aead1ad72 => 82
	i64 6911788284027924527, ; 279: Microsoft.AspNetCore.Hosting.Server.Abstractions => 0x5feb9ad2f830f02f => 199
	i64 6920570528939222495, ; 280: Microsoft.AspNetCore.Components.WebView => 0x600ace3ab475a5df => 192
	i64 7011053663211085209, ; 281: Xamarin.AndroidX.Fragment.Ktx => 0x614c442918e5dd99 => 284
	i64 7060448593242414269, ; 282: System.Security.Cryptography.Xml => 0x61fbc096731edcbd => 249
	i64 7060896174307865760, ; 283: System.Threading.Tasks.Parallel.dll => 0x61fd57a90988f4a0 => 143
	i64 7083547580668757502, ; 284: System.Private.Xml.Linq.dll => 0x624dd0fe8f56c5fe => 87
	i64 7101497697220435230, ; 285: System.Configuration => 0x628d9687c0141d1e => 19
	i64 7103753931438454322, ; 286: Xamarin.AndroidX.Interpolator.dll => 0x62959a90372c7632 => 285
	i64 7105430439328552570, ; 287: System.Security.Cryptography.Pkcs => 0x629b8f56a06d167a => 248
	i64 7112547816752919026, ; 288: System.IO.FileSystem => 0x62b4d88e3189b1f2 => 51
	i64 7192745174564810625, ; 289: Xamarin.Android.Glide.GifDecoder.dll => 0x63d1c3a0a1d72f81 => 254
	i64 7220009545223068405, ; 290: sv/Microsoft.Maui.Controls.resources.dll => 0x6432a06d99f35af5 => 361
	i64 7270811800166795866, ; 291: System.Linq => 0x64e71ccf51a90a5a => 61
	i64 7299370801165188114, ; 292: System.IO.Pipes.AccessControl.dll => 0x654c9311e74f3c12 => 54
	i64 7316205155833392065, ; 293: Microsoft.Win32.Primitives => 0x658861d38954abc1 => 4
	i64 7331765743953618630, ; 294: Microsoft.AspNetCore.Http.dll => 0x65bfaa1948bba6c6 => 200
	i64 7338192458477945005, ; 295: System.Reflection => 0x65d67f295d0740ad => 97
	i64 7349431895026339542, ; 296: Xamarin.Android.Glide.DiskLruCache => 0x65fe6d5e9bf88ed6 => 253
	i64 7377312882064240630, ; 297: System.ComponentModel.TypeConverter.dll => 0x66617afac45a2ff6 => 17
	i64 7488575175965059935, ; 298: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 155
	i64 7489048572193775167, ; 299: System.ObjectModel => 0x67ee71ff6b419e3f => 84
	i64 7592577537120840276, ; 300: System.Diagnostics.Process => 0x695e410af5b2aa54 => 29
	i64 7637303409920963731, ; 301: System.IO.Compression.ZipFile.dll => 0x69fd26fcb637f493 => 45
	i64 7637365915383206639, ; 302: Xamarin.Essentials.dll => 0x69fd5fd5e61792ef => 322
	i64 7654504624184590948, ; 303: System.Net.Http => 0x6a3a4366801b8264 => 64
	i64 7694700312542370399, ; 304: System.Net.Mail => 0x6ac9112a7e2cda5f => 66
	i64 7708790323521193081, ; 305: ms/Microsoft.Maui.Controls.resources.dll => 0x6afb1ff4d1730479 => 352
	i64 7714652370974252055, ; 306: System.Private.CoreLib => 0x6b0ff375198b9c17 => 172
	i64 7725404731275645577, ; 307: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x6b3626ac11ce9289 => 293
	i64 7735176074855944702, ; 308: Microsoft.CSharp => 0x6b58dda848e391fe => 1
	i64 7735352534559001595, ; 309: Xamarin.Kotlin.StdLib.dll => 0x6b597e2582ce8bfb => 329
	i64 7791074099216502080, ; 310: System.IO.FileSystem.AccessControl.dll => 0x6c1f749d468bcd40 => 47
	i64 7820441508502274321, ; 311: System.Data => 0x6c87ca1e14ff8111 => 24
	i64 7836164640616011524, ; 312: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x6cbfa6390d64d704 => 261
	i64 7972383140441761405, ; 313: Microsoft.Extensions.Caching.Abstractions.dll => 0x6ea3983a0b58267d => 209
	i64 8014722069583580780, ; 314: Microsoft.AspNetCore.Components.Forms.dll => 0x6f3a03422b034e6c => 187
	i64 8025517457475554965, ; 315: WindowsBase => 0x6f605d9b4786ce95 => 165
	i64 8031450141206250471, ; 316: System.Runtime.Intrinsics.dll => 0x6f757159d9dc03e7 => 108
	i64 8064050204834738623, ; 317: System.Collections.dll => 0x6fe942efa61731bf => 12
	i64 8083354569033831015, ; 318: Xamarin.AndroidX.Lifecycle.Common.dll => 0x702dd82730cad267 => 287
	i64 8085230611270010360, ; 319: System.Net.Http.Json.dll => 0x703482674fdd05f8 => 63
	i64 8087206902342787202, ; 320: System.Diagnostics.DiagnosticSource => 0x703b87d46f3aa082 => 27
	i64 8103644804370223335, ; 321: System.Data.DataSetExtensions.dll => 0x7075ee03be6d50e7 => 23
	i64 8113615946733131500, ; 322: System.Reflection.Extensions => 0x70995ab73cf916ec => 93
	i64 8167236081217502503, ; 323: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 168
	i64 8185542183669246576, ; 324: System.Collections => 0x7198e33f4794aa70 => 12
	i64 8187640529827139739, ; 325: Xamarin.KotlinX.Coroutines.Android => 0x71a057ae90f0109b => 333
	i64 8246048515196606205, ; 326: Microsoft.Maui.Graphics.dll => 0x726fd96f64ee56fd => 239
	i64 8264926008854159966, ; 327: System.Diagnostics.Process.dll => 0x72b2ea6a64a3a25e => 29
	i64 8290740647658429042, ; 328: System.Runtime.Extensions => 0x730ea0b15c929a72 => 103
	i64 8318905602908530212, ; 329: System.ComponentModel.DataAnnotations => 0x7372b092055ea624 => 14
	i64 8368701292315763008, ; 330: System.Security.Cryptography => 0x7423997c6fd56140 => 126
	i64 8398329775253868912, ; 331: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x748cdc6f3097d170 => 270
	i64 8399132193771933415, ; 332: Microsoft.Extensions.WebEncoders => 0x748fb63acf52cee7 => 231
	i64 8400357532724379117, ; 333: Xamarin.AndroidX.Navigation.UI.dll => 0x749410ab44503ded => 302
	i64 8406879421826901811, ; 334: Microsoft.JSInterop.WebAssembly.dll => 0x74ab3c4ae788a733 => 233
	i64 8410671156615598628, ; 335: System.Reflection.Emit.Lightweight.dll => 0x74b8b4daf4b25224 => 91
	i64 8426919725312979251, ; 336: Xamarin.AndroidX.Lifecycle.Process => 0x74f26ed7aa033133 => 291
	i64 8442828414178614895, ; 337: Microsoft.AspNetCore.Cryptography.KeyDerivation => 0x752af3b5eeb0de6f => 195
	i64 8476857680833348370, ; 338: System.Security.Permissions.dll => 0x75a3d925fd9d0312 => 250
	i64 8518412311883997971, ; 339: System.Collections.Immutable => 0x76377add7c28e313 => 9
	i64 8563666267364444763, ; 340: System.Private.Uri => 0x76d841191140ca5b => 86
	i64 8598790081731763592, ; 341: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x77550a055fc61d88 => 281
	i64 8601935802264776013, ; 342: Xamarin.AndroidX.Transition.dll => 0x7760370982b4ed4d => 314
	i64 8611142787134128904, ; 343: Microsoft.AspNetCore.Hosting.Server.Abstractions.dll => 0x7780ecbdb94c0308 => 199
	i64 8614108721271900878, ; 344: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x778b763e14018ace => 356
	i64 8623059219396073920, ; 345: System.Net.Quic.dll => 0x77ab42ac514299c0 => 71
	i64 8626175481042262068, ; 346: Java.Interop => 0x77b654e585b55834 => 168
	i64 8638972117149407195, ; 347: Microsoft.CSharp.dll => 0x77e3cb5e8b31d7db => 1
	i64 8639588376636138208, ; 348: Xamarin.AndroidX.Navigation.Runtime => 0x77e5fbdaa2fda2e0 => 301
	i64 8648495978913578441, ; 349: Microsoft.Win32.Registry.dll => 0x7805a1456889bdc9 => 5
	i64 8677882282824630478, ; 350: pt-BR\Microsoft.Maui.Controls.resources => 0x786e07f5766b00ce => 356
	i64 8684531736582871431, ; 351: System.IO.Compression.FileSystem => 0x7885a79a0fa0d987 => 44
	i64 8725526185868997716, ; 352: System.Diagnostics.DiagnosticSource.dll => 0x79174bd613173454 => 27
	i64 8853378295825400934, ; 353: Xamarin.Kotlin.StdLib.Common.dll => 0x7add84a720d38466 => 330
	i64 8941376889969657626, ; 354: System.Xml.XDocument => 0x7c1626e87187471a => 158
	i64 8951477988056063522, ; 355: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0x7c3a09cd9ccf5e22 => 304
	i64 8954753533646919997, ; 356: System.Runtime.Serialization.Json => 0x7c45ace50032d93d => 112
	i64 9045785047181495996, ; 357: zh-HK\Microsoft.Maui.Controls.resources => 0x7d891592e3cb0ebc => 366
	i64 9111603110219107042, ; 358: Microsoft.Extensions.Caching.Memory => 0x7e72eac0def44ae2 => 210
	i64 9138683372487561558, ; 359: System.Security.Cryptography.Csp => 0x7ed3201bc3e3d156 => 121
	i64 9250544137016314866, ; 360: Microsoft.EntityFrameworkCore => 0x806088e191ee0bf2 => 207
	i64 9312692141327339315, ; 361: Xamarin.AndroidX.ViewPager2 => 0x813d54296a634f33 => 319
	i64 9324707631942237306, ; 362: Xamarin.AndroidX.AppCompat => 0x8168042fd44a7c7a => 260
	i64 9413000421947348542, ; 363: Microsoft.AspNetCore.Hosting.Abstractions.dll => 0x82a1b202f4c6163e => 198
	i64 9468215723722196442, ; 364: System.Xml.XPath.XDocument.dll => 0x8365dc09353ac5da => 159
	i64 9508211702228543126, ; 365: Microsoft.AspNetCore.Cryptography.KeyDerivation.dll => 0x83f3f42aa08b6696 => 195
	i64 9554839972845591462, ; 366: System.ServiceModel.Web => 0x84999c54e32a1ba6 => 131
	i64 9575902398040817096, ; 367: Xamarin.Google.Crypto.Tink.Android.dll => 0x84e4707ee708bdc8 => 325
	i64 9584643793929893533, ; 368: System.IO.dll => 0x85037ebfbbd7f69d => 57
	i64 9643464320970503839, ; 369: Microsoft.AspNet.Identity.EntityFramework => 0x85d477b4e799b69f => 179
	i64 9650158550865574924, ; 370: Microsoft.Extensions.Configuration.Json => 0x85ec4012c28a7c0c => 215
	i64 9659729154652888475, ; 371: System.Text.RegularExpressions => 0x860e407c9991dd9b => 138
	i64 9662334977499516867, ; 372: System.Numerics.dll => 0x8617827802b0cfc3 => 83
	i64 9667360217193089419, ; 373: System.Diagnostics.StackTrace => 0x86295ce5cd89898b => 30
	i64 9678050649315576968, ; 374: Xamarin.AndroidX.CoordinatorLayout.dll => 0x864f57c9feb18c88 => 271
	i64 9702891218465930390, ; 375: System.Collections.NonGeneric.dll => 0x86a79827b2eb3c96 => 10
	i64 9780093022148426479, ; 376: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x87b9dec9576efaef => 321
	i64 9808709177481450983, ; 377: Mono.Android.dll => 0x881f890734e555e7 => 171
	i64 9814504515138190298, ; 378: Microsoft.AspNetCore.Authentication.Cookies => 0x88341fdb67477bda => 182
	i64 9825649861376906464, ; 379: Xamarin.AndroidX.Concurrent.Futures => 0x885bb87d8abc94e0 => 268
	i64 9834056768316610435, ; 380: System.Transactions.dll => 0x8879968718899783 => 150
	i64 9836529246295212050, ; 381: System.Reflection.Metadata => 0x88825f3bbc2ac012 => 94
	i64 9864374015518639636, ; 382: Microsoft.AspNetCore.Cryptography.Internal => 0x88e54be746950614 => 194
	i64 9907349773706910547, ; 383: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x897dfa20b758db53 => 281
	i64 9924019376719386149, ; 384: EntityFramework => 0x89b9330b1d73d625 => 176
	i64 9933555792566666578, ; 385: System.Linq.Queryable.dll => 0x89db145cf475c552 => 60
	i64 9956195530459977388, ; 386: Microsoft.Maui => 0x8a2b8315b36616ac => 237
	i64 9974604633896246661, ; 387: System.Xml.Serialization.dll => 0x8a6cea111a59dd85 => 157
	i64 9991543690424095600, ; 388: es/Microsoft.Maui.Controls.resources.dll => 0x8aa9180c89861370 => 341
	i64 10017511394021241210, ; 389: Microsoft.Extensions.Logging.Debug => 0x8b055989ae10717a => 227
	i64 10038780035334861115, ; 390: System.Net.Http.dll => 0x8b50e941206af13b => 64
	i64 10051358222726253779, ; 391: System.Private.Xml => 0x8b7d990c97ccccd3 => 88
	i64 10078727084704864206, ; 392: System.Net.WebSockets.Client => 0x8bded4e257f117ce => 79
	i64 10089571585547156312, ; 393: System.IO.FileSystem.AccessControl => 0x8c055be67469bb58 => 47
	i64 10092835686693276772, ; 394: Microsoft.Maui.Controls => 0x8c10f49539bd0c64 => 235
	i64 10105485790837105934, ; 395: System.Threading.Tasks.Parallel => 0x8c3de5c91d9a650e => 143
	i64 10143853363526200146, ; 396: da\Microsoft.Maui.Controls.resources => 0x8cc634e3c2a16b52 => 338
	i64 10205853378024263619, ; 397: Microsoft.Extensions.Configuration.Binder => 0x8da279930adb4fc3 => 213
	i64 10226222362177979215, ; 398: Xamarin.Kotlin.StdLib.Jdk7 => 0x8dead70ebbc6434f => 331
	i64 10229024438826829339, ; 399: Xamarin.AndroidX.CustomView => 0x8df4cb880b10061b => 275
	i64 10236703004850800690, ; 400: System.Net.ServicePoint.dll => 0x8e101325834e4832 => 74
	i64 10243523786148452761, ; 401: Microsoft.AspNetCore.Http.Abstractions => 0x8e284e9c69a49999 => 201
	i64 10245369515835430794, ; 402: System.Reflection.Emit.Lightweight => 0x8e2edd4ad7fc978a => 91
	i64 10321854143672141184, ; 403: Xamarin.Jetbrains.Annotations.dll => 0x8f3e97a7f8f8c580 => 328
	i64 10360651442923773544, ; 404: System.Text.Encoding => 0x8fc86d98211c1e68 => 135
	i64 10364469296367737616, ; 405: System.Reflection.Emit.ILGeneration.dll => 0x8fd5fde967711b10 => 90
	i64 10376576884623852283, ; 406: Xamarin.AndroidX.Tracing.Tracing => 0x900101b2f888c2fb => 313
	i64 10406448008575299332, ; 407: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x906b2153fcb3af04 => 334
	i64 10430153318873392755, ; 408: Xamarin.AndroidX.Core => 0x90bf592ea44f6673 => 272
	i64 10485858537740568411, ; 409: Microsoft.AspNetCore.Identity => 0x918540c89b06075b => 204
	i64 10506226065143327199, ; 410: ca\Microsoft.Maui.Controls.resources => 0x91cd9cf11ed169df => 336
	i64 10546663366131771576, ; 411: System.Runtime.Serialization.Json.dll => 0x925d4673efe8e8b8 => 112
	i64 10566960649245365243, ; 412: System.Globalization.dll => 0x92a562b96dcd13fb => 42
	i64 10595762989148858956, ; 413: System.Xml.XPath.XDocument => 0x930bb64cc472ea4c => 159
	i64 10650478070646097812, ; 414: System.IO.Packaging => 0x93ce196068f2c794 => 246
	i64 10670374202010151210, ; 415: Microsoft.Win32.Primitives.dll => 0x9414c8cd7b4ea92a => 4
	i64 10714184849103829812, ; 416: System.Runtime.Extensions.dll => 0x94b06e5aa4b4bb34 => 103
	i64 10722976995944589488, ; 417: EntityFramework.dll => 0x94cfaac3d9f668b0 => 176
	i64 10734191584620811116, ; 418: Microsoft.Extensions.FileProviders.Embedded.dll => 0x94f7825fc04f936c => 220
	i64 10785150219063592792, ; 419: System.Net.Primitives => 0x95ac8cfb68830758 => 70
	i64 10822644899632537592, ; 420: System.Linq.Queryable => 0x9631c23204ca5ff8 => 60
	i64 10830817578243619689, ; 421: System.Formats.Tar => 0x964ecb340a447b69 => 39
	i64 10847732767863316357, ; 422: Xamarin.AndroidX.Arch.Core.Common => 0x968ae37a86db9f85 => 262
	i64 10899834349646441345, ; 423: System.Web => 0x9743fd975946eb81 => 153
	i64 10943875058216066601, ; 424: System.IO.UnmanagedMemoryStream.dll => 0x97e07461df39de29 => 56
	i64 10964653383833615866, ; 425: System.Diagnostics.Tracing => 0x982a4628ccaffdfa => 34
	i64 11002576679268595294, ; 426: Microsoft.Extensions.Logging.Abstractions => 0x98b1013215cd365e => 226
	i64 11009005086950030778, ; 427: Microsoft.Maui.dll => 0x98c7d7cc621ffdba => 237
	i64 11019817191295005410, ; 428: Xamarin.AndroidX.Annotation.Jvm.dll => 0x98ee415998e1b2e2 => 259
	i64 11023048688141570732, ; 429: System.Core => 0x98f9bc61168392ac => 21
	i64 11037814507248023548, ; 430: System.Xml => 0x992e31d0412bf7fc => 163
	i64 11050168729868392624, ; 431: Microsoft.AspNetCore.Http.Features => 0x995a15e9dbef58b0 => 203
	i64 11051904132540108364, ; 432: Microsoft.Extensions.FileProviders.Composite.dll => 0x99604040c7b98e4c => 219
	i64 11071824625609515081, ; 433: Xamarin.Google.ErrorProne.Annotations => 0x99a705d600e0a049 => 326
	i64 11103970607964515343, ; 434: hu\Microsoft.Maui.Controls.resources => 0x9a193a6fc41a6c0f => 347
	i64 11136029745144976707, ; 435: Jsr305Binding.dll => 0x9a8b200d4f8cd543 => 324
	i64 11162124722117608902, ; 436: Xamarin.AndroidX.ViewPager => 0x9ae7d54b986d05c6 => 318
	i64 11188319605227840848, ; 437: System.Threading.Overlapped => 0x9b44e5671724e550 => 140
	i64 11218356222449480316, ; 438: Microsoft.AspNetCore.Components => 0x9baf9b8c02e4f27c => 185
	i64 11220793807500858938, ; 439: ja\Microsoft.Maui.Controls.resources => 0x9bb8448481fdd63a => 350
	i64 11226290749488709958, ; 440: Microsoft.Extensions.Options.dll => 0x9bcbcbf50c874146 => 229
	i64 11235648312900863002, ; 441: System.Reflection.DispatchProxy.dll => 0x9bed0a9c8fac441a => 89
	i64 11293348803414341309, ; 442: Microsoft.AspNetCore.Identity.dll => 0x9cba08e6e81836bd => 204
	i64 11329751333533450475, ; 443: System.Threading.Timer.dll => 0x9d3b5ccf6cc500eb => 147
	i64 11340910727871153756, ; 444: Xamarin.AndroidX.CursorAdapter => 0x9d630238642d465c => 274
	i64 11347436699239206956, ; 445: System.Xml.XmlSerializer.dll => 0x9d7a318e8162502c => 162
	i64 11392833485892708388, ; 446: Xamarin.AndroidX.Print.dll => 0x9e1b79b18fcf6824 => 303
	i64 11432101114902388181, ; 447: System.AppContext => 0x9ea6fb64e61a9dd5 => 6
	i64 11446671985764974897, ; 448: Mono.Android.Export => 0x9edabf8623efc131 => 169
	i64 11448276831755070604, ; 449: System.Diagnostics.TextWriterTraceListener => 0x9ee0731f77186c8c => 31
	i64 11485890710487134646, ; 450: System.Runtime.InteropServices => 0x9f6614bf0f8b71b6 => 107
	i64 11508496261504176197, ; 451: Xamarin.AndroidX.Fragment.Ktx.dll => 0x9fb664600dde1045 => 284
	i64 11513602507638267977, ; 452: System.IO.Pipelines.dll => 0x9fc8887aa0d36049 => 247
	i64 11518296021396496455, ; 453: id\Microsoft.Maui.Controls.resources => 0x9fd9353475222047 => 348
	i64 11529969570048099689, ; 454: Xamarin.AndroidX.ViewPager.dll => 0xa002ae3c4dc7c569 => 318
	i64 11530571088791430846, ; 455: Microsoft.Extensions.Logging => 0xa004d1504ccd66be => 225
	i64 11580057168383206117, ; 456: Xamarin.AndroidX.Annotation => 0xa0b4a0a4103262e5 => 257
	i64 11591352189662810718, ; 457: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0xa0dcc167234c525e => 311
	i64 11597308262950669618, ; 458: Microsoft.Extensions.Identity.Core.dll => 0xa0f1ea6b83e08d32 => 224
	i64 11597940890313164233, ; 459: netstandard => 0xa0f429ca8d1805c9 => 167
	i64 11672361001936329215, ; 460: Xamarin.AndroidX.Interpolator => 0xa1fc8e7d0a8999ff => 285
	i64 11692977985522001935, ; 461: System.Threading.Overlapped.dll => 0xa245cd869980680f => 140
	i64 11705530742807338875, ; 462: he/Microsoft.Maui.Controls.resources.dll => 0xa272663128721f7b => 344
	i64 11707554492040141440, ; 463: System.Linq.Parallel.dll => 0xa27996c7fe94da80 => 59
	i64 11739066727115742305, ; 464: SQLite-net.dll => 0xa2e98afdf8575c61 => 241
	i64 11743665907891708234, ; 465: System.Threading.Tasks => 0xa2f9e1ec30c0214a => 144
	i64 11806260347154423189, ; 466: SQLite-net => 0xa3d8433bc5eb5d95 => 241
	i64 11991047634523762324, ; 467: System.Net => 0xa668c24ad493ae94 => 81
	i64 12040886584167504988, ; 468: System.Net.ServicePoint => 0xa719d28d8e121c5c => 74
	i64 12048689113179125827, ; 469: Microsoft.Extensions.FileProviders.Physical => 0xa7358ae968287843 => 221
	i64 12058074296353848905, ; 470: Microsoft.Extensions.FileSystemGlobbing.dll => 0xa756e2afa5707e49 => 222
	i64 12063623837170009990, ; 471: System.Security => 0xa76a99f6ce740786 => 130
	i64 12096697103934194533, ; 472: System.Diagnostics.Contracts => 0xa7e019eccb7e8365 => 25
	i64 12102847907131387746, ; 473: System.Buffers => 0xa7f5f40c43256f62 => 7
	i64 12123043025855404482, ; 474: System.Reflection.Extensions.dll => 0xa83db366c0e359c2 => 93
	i64 12137774235383566651, ; 475: Xamarin.AndroidX.VectorDrawable => 0xa872095bbfed113b => 315
	i64 12145679461940342714, ; 476: System.Text.Json => 0xa88e1f1ebcb62fba => 137
	i64 12191646537372739477, ; 477: Xamarin.Android.Glide.dll => 0xa9316dee7f392795 => 251
	i64 12201331334810686224, ; 478: System.Runtime.Serialization.Primitives.dll => 0xa953d6341e3bd310 => 113
	i64 12269460666702402136, ; 479: System.Collections.Immutable.dll => 0xaa45e178506c9258 => 9
	i64 12279246230491828964, ; 480: SQLitePCLRaw.provider.e_sqlite3.dll => 0xaa68a5636e0512e4 => 245
	i64 12313367145828839434, ; 481: System.IO.Pipelines => 0xaae1de2e1c17f00a => 247
	i64 12332222936682028543, ; 482: System.Runtime.Handles => 0xab24db6c07db5dff => 104
	i64 12375446203996702057, ; 483: System.Configuration.dll => 0xabbe6ac12e2e0569 => 19
	i64 12441092376399691269, ; 484: Microsoft.AspNetCore.Authentication.Abstractions.dll => 0xaca7a399c11fbe05 => 181
	i64 12451044538927396471, ; 485: Xamarin.AndroidX.Fragment.dll => 0xaccaff0a2955b677 => 283
	i64 12459959602091767212, ; 486: Microsoft.AspNetCore.Components.Authorization.dll => 0xaceaab3e0e65b5ac => 186
	i64 12466513435562512481, ; 487: Xamarin.AndroidX.Loader.dll => 0xad01f3eb52569061 => 297
	i64 12475113361194491050, ; 488: _Microsoft.Android.Resource.Designer.dll => 0xad2081818aba1caa => 372
	i64 12487638416075308985, ; 489: Xamarin.AndroidX.DocumentFile.dll => 0xad4d00fa21b0bfb9 => 277
	i64 12517810545449516888, ; 490: System.Diagnostics.TraceSource.dll => 0xadb8325e6f283f58 => 33
	i64 12538491095302438457, ; 491: Xamarin.AndroidX.CardView.dll => 0xae01ab382ae67e39 => 265
	i64 12550732019250633519, ; 492: System.IO.Compression => 0xae2d28465e8e1b2f => 46
	i64 12681088699309157496, ; 493: it/Microsoft.Maui.Controls.resources.dll => 0xaffc46fc178aec78 => 349
	i64 12699999919562409296, ; 494: System.Diagnostics.StackTrace.dll => 0xb03f76a3ad01c550 => 30
	i64 12700543734426720211, ; 495: Xamarin.AndroidX.Collection => 0xb041653c70d157d3 => 266
	i64 12708238894395270091, ; 496: System.IO => 0xb05cbbf17d3ba3cb => 57
	i64 12708922737231849740, ; 497: System.Text.Encoding.Extensions => 0xb05f29e50e96e90c => 134
	i64 12717050818822477433, ; 498: System.Runtime.Serialization.Xml.dll => 0xb07c0a5786811679 => 114
	i64 12753841065332862057, ; 499: Xamarin.AndroidX.Window => 0xb0febee04cf46c69 => 320
	i64 12823819093633476069, ; 500: th/Microsoft.Maui.Controls.resources.dll => 0xb1f75b85abe525e5 => 362
	i64 12828192437253469131, ; 501: Xamarin.Kotlin.StdLib.Jdk8.dll => 0xb206e50e14d873cb => 332
	i64 12835242264250840079, ; 502: System.IO.Pipes => 0xb21ff0d5d6c0740f => 55
	i64 12843321153144804894, ; 503: Microsoft.Extensions.Primitives => 0xb23ca48abd74d61e => 230
	i64 12843770487262409629, ; 504: System.AppContext.dll => 0xb23e3d357debf39d => 6
	i64 12859557719246324186, ; 505: System.Net.WebHeaderCollection.dll => 0xb276539ce04f41da => 77
	i64 12982280885948128408, ; 506: Xamarin.AndroidX.CustomView.PoolingContainer => 0xb42a53aec5481c98 => 276
	i64 13003699287675270979, ; 507: Microsoft.AspNetCore.Components.WebView.Maui => 0xb4766b9b07e27743 => 193
	i64 13068258254871114833, ; 508: System.Runtime.Serialization.Formatters.dll => 0xb55bc7a4eaa8b451 => 111
	i64 13086625805112021739, ; 509: Microsoft.AspNetCore.DataProtection.Abstractions.dll => 0xb59d08d5762992eb => 197
	i64 13129914918964716986, ; 510: Xamarin.AndroidX.Emoji2.dll => 0xb636d40db3fe65ba => 280
	i64 13162471042547327635, ; 511: System.Security.Permissions => 0xb6aa7dace9662293 => 250
	i64 13173818576982874404, ; 512: System.Runtime.CompilerServices.VisualC.dll => 0xb6d2ce32a8819924 => 102
	i64 13221551921002590604, ; 513: ca/Microsoft.Maui.Controls.resources.dll => 0xb77c636bdebe318c => 336
	i64 13222659110913276082, ; 514: ja/Microsoft.Maui.Controls.resources.dll => 0xb78052679c1178b2 => 350
	i64 13343850469010654401, ; 515: Mono.Android.Runtime.dll => 0xb92ee14d854f44c1 => 170
	i64 13370592475155966277, ; 516: System.Runtime.Serialization => 0xb98de304062ea945 => 115
	i64 13381594904270902445, ; 517: he\Microsoft.Maui.Controls.resources => 0xb9b4f9aaad3e94ad => 344
	i64 13401370062847626945, ; 518: Xamarin.AndroidX.VectorDrawable.dll => 0xb9fb3b1193964ec1 => 315
	i64 13404347523447273790, ; 519: Xamarin.AndroidX.ConstraintLayout.Core => 0xba05cf0da4f6393e => 270
	i64 13404984788036896679, ; 520: Microsoft.AspNetCore.Http.Abstractions.dll => 0xba0812a45e7447a7 => 201
	i64 13431476299110033919, ; 521: System.Net.WebClient => 0xba663087f18829ff => 76
	i64 13454009404024712428, ; 522: Xamarin.Google.Guava.ListenableFuture => 0xbab63e4543a86cec => 327
	i64 13463706743370286408, ; 523: System.Private.DataContractSerialization.dll => 0xbad8b1f3069e0548 => 85
	i64 13465488254036897740, ; 524: Xamarin.Kotlin.StdLib => 0xbadf06394d106fcc => 329
	i64 13467053111158216594, ; 525: uk/Microsoft.Maui.Controls.resources.dll => 0xbae49573fde79792 => 364
	i64 13491513212026656886, ; 526: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0xbb3b7bc905569876 => 263
	i64 13540124433173649601, ; 527: vi\Microsoft.Maui.Controls.resources => 0xbbe82f6eede718c1 => 365
	i64 13545416393490209236, ; 528: id/Microsoft.Maui.Controls.resources.dll => 0xbbfafc7174bc99d4 => 348
	i64 13550417756503177631, ; 529: Microsoft.Extensions.FileProviders.Abstractions.dll => 0xbc0cc1280684799f => 218
	i64 13572454107664307259, ; 530: Xamarin.AndroidX.RecyclerView.dll => 0xbc5b0b19d99f543b => 305
	i64 13578472628727169633, ; 531: System.Xml.XPath => 0xbc706ce9fba5c261 => 160
	i64 13580399111273692417, ; 532: Microsoft.VisualBasic.Core.dll => 0xbc77450a277fbd01 => 2
	i64 13621154251410165619, ; 533: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0xbd080f9faa1acf73 => 276
	i64 13647894001087880694, ; 534: System.Data.dll => 0xbd670f48cb071df6 => 24
	i64 13675589307506966157, ; 535: Xamarin.AndroidX.Activity.Ktx => 0xbdc97404d0153e8d => 256
	i64 13702626353344114072, ; 536: System.Diagnostics.Tools.dll => 0xbe29821198fb6d98 => 32
	i64 13710614125866346983, ; 537: System.Security.AccessControl.dll => 0xbe45e2e7d0b769e7 => 117
	i64 13713329104121190199, ; 538: System.Dynamic.Runtime => 0xbe4f8829f32b5737 => 37
	i64 13717397318615465333, ; 539: System.ComponentModel.Primitives.dll => 0xbe5dfc2ef2f87d75 => 16
	i64 13755568601956062840, ; 540: fr/Microsoft.Maui.Controls.resources.dll => 0xbee598c36b1b9678 => 343
	i64 13768883594457632599, ; 541: System.IO.IsolatedStorage => 0xbf14e6adb159cf57 => 52
	i64 13814445057219246765, ; 542: hr/Microsoft.Maui.Controls.resources.dll => 0xbfb6c49664b43aad => 346
	i64 13828521679616088467, ; 543: Xamarin.Kotlin.StdLib.Common => 0xbfe8c733724e1993 => 330
	i64 13881769479078963060, ; 544: System.Console.dll => 0xc0a5f3cade5c6774 => 20
	i64 13911222732217019342, ; 545: System.Security.Cryptography.OpenSsl.dll => 0xc10e975ec1226bce => 123
	i64 13921917134693230900, ; 546: Microsoft.AspNetCore.WebUtilities => 0xc13495df5dd06934 => 206
	i64 13928444506500929300, ; 547: System.Windows.dll => 0xc14bc67b8bba9714 => 154
	i64 13959074834287824816, ; 548: Xamarin.AndroidX.Fragment => 0xc1b8989a7ad20fb0 => 283
	i64 14075334701871371868, ; 549: System.ServiceModel.Web.dll => 0xc355a25647c5965c => 131
	i64 14082136096249122791, ; 550: Microsoft.AspNetCore.Metadata => 0xc36dcc2b4fa28fe7 => 205
	i64 14100563506285742564, ; 551: da/Microsoft.Maui.Controls.resources.dll => 0xc3af43cd0cff89e4 => 338
	i64 14124974489674258913, ; 552: Xamarin.AndroidX.CardView => 0xc405fd76067d19e1 => 265
	i64 14125464355221830302, ; 553: System.Threading.dll => 0xc407bafdbc707a9e => 148
	i64 14133832980772275001, ; 554: Microsoft.EntityFrameworkCore.dll => 0xc425763635a1c339 => 207
	i64 14148919944076435199, ; 555: DocumentFormat.OpenXml.dll => 0xc45b0fb9961d9eff => 173
	i64 14178052285788134900, ; 556: Xamarin.Android.Glide.Annotations.dll => 0xc4c28f6f75511df4 => 252
	i64 14212104595480609394, ; 557: System.Security.Cryptography.Cng.dll => 0xc53b89d4a4518272 => 120
	i64 14220608275227875801, ; 558: System.Diagnostics.FileVersionInfo.dll => 0xc559bfe1def019d9 => 28
	i64 14226382999226559092, ; 559: System.ServiceProcess => 0xc56e43f6938e2a74 => 132
	i64 14232023429000439693, ; 560: System.Resources.Writer.dll => 0xc5824de7789ba78d => 100
	i64 14254574811015963973, ; 561: System.Text.Encoding.Extensions.dll => 0xc5d26c4442d66545 => 134
	i64 14261073672896646636, ; 562: Xamarin.AndroidX.Print => 0xc5e982f274ae0dec => 303
	i64 14298246716367104064, ; 563: System.Web.dll => 0xc66d93a217f4e840 => 153
	i64 14327695147300244862, ; 564: System.Reflection.dll => 0xc6d632d338eb4d7e => 97
	i64 14327709162229390963, ; 565: System.Security.Cryptography.X509Certificates => 0xc6d63f9253cade73 => 125
	i64 14331727281556788554, ; 566: Xamarin.Android.Glide.DiskLruCache.dll => 0xc6e48607a2f7954a => 253
	i64 14346402571976470310, ; 567: System.Net.Ping.dll => 0xc718a920f3686f26 => 69
	i64 14461014870687870182, ; 568: System.Net.Requests.dll => 0xc8afd8683afdece6 => 72
	i64 14464374589798375073, ; 569: ru\Microsoft.Maui.Controls.resources => 0xc8bbc80dcb1e5ea1 => 359
	i64 14486659737292545672, ; 570: Xamarin.AndroidX.Lifecycle.LiveData => 0xc90af44707469e88 => 288
	i64 14495724990987328804, ; 571: Xamarin.AndroidX.ResourceInspection.Annotation => 0xc92b2913e18d5d24 => 306
	i64 14522721392235705434, ; 572: el/Microsoft.Maui.Controls.resources.dll => 0xc98b12295c2cf45a => 340
	i64 14551742072151931844, ; 573: System.Text.Encodings.Web.dll => 0xc9f22c50f1b8fbc4 => 136
	i64 14561513370130550166, ; 574: System.Security.Cryptography.Primitives.dll => 0xca14e3428abb8d96 => 124
	i64 14574160591280636898, ; 575: System.Net.Quic => 0xca41d1d72ec783e2 => 71
	i64 14622043554576106986, ; 576: System.Runtime.Serialization.Formatters => 0xcaebef2458cc85ea => 111
	i64 14644440854989303794, ; 577: Xamarin.AndroidX.LocalBroadcastManager.dll => 0xcb3b815e37daeff2 => 298
	i64 14669215534098758659, ; 578: Microsoft.Extensions.DependencyInjection.dll => 0xcb9385ceb3993c03 => 216
	i64 14690985099581930927, ; 579: System.Web.HttpUtility => 0xcbe0dd1ca5233daf => 152
	i64 14705122255218365489, ; 580: ko\Microsoft.Maui.Controls.resources => 0xcc1316c7b0fb5431 => 351
	i64 14744092281598614090, ; 581: zh-Hans\Microsoft.Maui.Controls.resources => 0xcc9d89d004439a4a => 367
	i64 14792063746108907174, ; 582: Xamarin.Google.Guava.ListenableFuture.dll => 0xcd47f79af9c15ea6 => 327
	i64 14832630590065248058, ; 583: System.Security.Claims => 0xcdd816ef5d6e873a => 118
	i64 14852515768018889994, ; 584: Xamarin.AndroidX.CursorAdapter.dll => 0xce1ebc6625a76d0a => 274
	i64 14889905118082851278, ; 585: GoogleGson.dll => 0xcea391d0969961ce => 177
	i64 14892012299694389861, ; 586: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xceab0e490a083a65 => 368
	i64 14895643372947136109, ; 587: ProdutosMAUI.dll => 0xceb7f4ba296a5e6d => 0
	i64 14904040806490515477, ; 588: ar\Microsoft.Maui.Controls.resources => 0xced5ca2604cb2815 => 335
	i64 14912225920358050525, ; 589: System.Security.Principal.Windows => 0xcef2de7759506add => 127
	i64 14935719434541007538, ; 590: System.Text.Encoding.CodePages.dll => 0xcf4655b160b702b2 => 133
	i64 14954917835170835695, ; 591: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xcf8a8a895a82ecef => 217
	i64 14984936317414011727, ; 592: System.Net.WebHeaderCollection => 0xcff5302fe54ff34f => 77
	i64 14987728460634540364, ; 593: System.IO.Compression.dll => 0xcfff1ba06622494c => 46
	i64 14988210264188246988, ; 594: Xamarin.AndroidX.DocumentFile => 0xd000d1d307cddbcc => 277
	i64 15015154896917945444, ; 595: System.Net.Security.dll => 0xd0608bd33642dc64 => 73
	i64 15024878362326791334, ; 596: System.Net.Http.Json => 0xd0831743ebf0f4a6 => 63
	i64 15071021337266399595, ; 597: System.Resources.Reader.dll => 0xd127060e7a18a96b => 98
	i64 15076659072870671916, ; 598: System.ObjectModel.dll => 0xd13b0d8c1620662c => 84
	i64 15111608613780139878, ; 599: ms\Microsoft.Maui.Controls.resources => 0xd1b737f831192f66 => 352
	i64 15115185479366240210, ; 600: System.IO.Compression.Brotli.dll => 0xd1c3ed1c1bc467d2 => 43
	i64 15133485256822086103, ; 601: System.Linq.dll => 0xd204f0a9127dd9d7 => 61
	i64 15150743910298169673, ; 602: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xd2424150783c3149 => 304
	i64 15227001540531775957, ; 603: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd3512d3999b8e9d5 => 212
	i64 15234786388537674379, ; 604: System.Dynamic.Runtime.dll => 0xd36cd580c5be8a8b => 37
	i64 15250465174479574862, ; 605: System.Globalization.Calendars.dll => 0xd3a489469852174e => 40
	i64 15272359115529052076, ; 606: Xamarin.AndroidX.Collection.Ktx => 0xd3f251b2fb4edfac => 267
	i64 15279429628684179188, ; 607: Xamarin.KotlinX.Coroutines.Android.dll => 0xd40b704b1c4c96f4 => 333
	i64 15299439993936780255, ; 608: System.Xml.XPath.dll => 0xd452879d55019bdf => 160
	i64 15338463749992804988, ; 609: System.Resources.Reader => 0xd4dd2b839286f27c => 98
	i64 15355483035186022585, ; 610: Microsoft.AspNet.Identity.Core => 0xd519a276b2ccc8b9 => 178
	i64 15370334346939861994, ; 611: Xamarin.AndroidX.Core.dll => 0xd54e65a72c560bea => 272
	i64 15391712275433856905, ; 612: Microsoft.Extensions.DependencyInjection.Abstractions => 0xd59a58c406411f89 => 217
	i64 15427448221306938193, ; 613: Microsoft.AspNetCore.Components.Web => 0xd6194e6b4dbb6351 => 190
	i64 15481710163200268842, ; 614: Microsoft.Extensions.FileProviders.Composite => 0xd6da155e291b5a2a => 219
	i64 15526743539506359484, ; 615: System.Text.Encoding.dll => 0xd77a12fc26de2cbc => 135
	i64 15527772828719725935, ; 616: System.Console => 0xd77dbb1e38cd3d6f => 20
	i64 15530465045505749832, ; 617: System.Net.HttpListener.dll => 0xd7874bacc9fdb348 => 65
	i64 15536481058354060254, ; 618: de\Microsoft.Maui.Controls.resources => 0xd79cab34eec75bde => 339
	i64 15541854775306130054, ; 619: System.Security.Cryptography.X509Certificates.dll => 0xd7afc292e8d49286 => 125
	i64 15557562860424774966, ; 620: System.Net.Sockets => 0xd7e790fe7a6dc536 => 75
	i64 15565247197164990907, ; 621: Microsoft.AspNetCore.Http.Extensions => 0xd802dddb8c29f1bb => 202
	i64 15582737692548360875, ; 622: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xd841015ed86f6aab => 296
	i64 15592226634512578529, ; 623: Microsoft.AspNetCore.Authorization.dll => 0xd862b7834f81b7e1 => 184
	i64 15609085926864131306, ; 624: System.dll => 0xd89e9cf3334914ea => 164
	i64 15661133872274321916, ; 625: System.Xml.ReaderWriter.dll => 0xd9578647d4bfb1fc => 156
	i64 15664356999916475676, ; 626: de/Microsoft.Maui.Controls.resources.dll => 0xd962f9b2b6ecd51c => 339
	i64 15710114879900314733, ; 627: Microsoft.Win32.Registry => 0xda058a3f5d096c6d => 5
	i64 15743187114543869802, ; 628: hu/Microsoft.Maui.Controls.resources.dll => 0xda7b09450ae4ef6a => 347
	i64 15755368083429170162, ; 629: System.IO.FileSystem.Primitives => 0xdaa64fcbde529bf2 => 49
	i64 15777549416145007739, ; 630: Xamarin.AndroidX.SlidingPaneLayout.dll => 0xdaf51d99d77eb47b => 310
	i64 15783653065526199428, ; 631: el\Microsoft.Maui.Controls.resources => 0xdb0accd674b1c484 => 340
	i64 15817206913877585035, ; 632: System.Threading.Tasks.dll => 0xdb8201e29086ac8b => 144
	i64 15827202283623377193, ; 633: Microsoft.Extensions.Configuration.Json.dll => 0xdba5849eef9f6929 => 215
	i64 15847085070278954535, ; 634: System.Threading.Channels.dll => 0xdbec27e8f35f8e27 => 139
	i64 15852824340364052161, ; 635: Microsoft.AspNetCore.Http.Features.dll => 0xdc008bbee610c6c1 => 203
	i64 15885744048853936810, ; 636: System.Resources.Writer => 0xdc75800bd0b6eaaa => 100
	i64 15917157041579513718, ; 637: Microsoft.AspNetCore.Authentication.Cookies.dll => 0xdce519ff21745776 => 182
	i64 15928521404965645318, ; 638: Microsoft.Maui.Controls.Compatibility => 0xdd0d79d32c2eec06 => 234
	i64 15934062614519587357, ; 639: System.Security.Cryptography.OpenSsl => 0xdd2129868f45a21d => 123
	i64 15937190497610202713, ; 640: System.Security.Cryptography.Cng => 0xdd2c465197c97e59 => 120
	i64 15963349826457351533, ; 641: System.Threading.Tasks.Extensions => 0xdd893616f748b56d => 142
	i64 15971679995444160383, ; 642: System.Formats.Tar.dll => 0xdda6ce5592a9677f => 39
	i64 16018552496348375205, ; 643: System.Net.NetworkInformation.dll => 0xde4d54a020caa8a5 => 68
	i64 16027684189145026053, ; 644: Microsoft.AspNetCore.DataProtection => 0xde6dc5da0a224e05 => 196
	i64 16046481083542319511, ; 645: Microsoft.Extensions.ObjectPool => 0xdeb08d870f90b197 => 228
	i64 16054465462676478687, ; 646: System.Globalization.Extensions => 0xdecceb47319bdadf => 41
	i64 16153500642854367575, ; 647: Microsoft.Extensions.WebEncoders.dll => 0xe02cc33ff060f157 => 231
	i64 16154507427712707110, ; 648: System => 0xe03056ea4e39aa26 => 164
	i64 16219561732052121626, ; 649: System.Net.Security => 0xe1177575db7c781a => 73
	i64 16288847719894691167, ; 650: nb\Microsoft.Maui.Controls.resources => 0xe20d9cb300c12d5f => 353
	i64 16315482530584035869, ; 651: WindowsBase.dll => 0xe26c3ceb1e8d821d => 165
	i64 16321164108206115771, ; 652: Microsoft.Extensions.Logging.Abstractions.dll => 0xe2806c487e7b0bbb => 226
	i64 16337011941688632206, ; 653: System.Security.Principal.Windows.dll => 0xe2b8b9cdc3aa638e => 127
	i64 16361933716545543812, ; 654: Xamarin.AndroidX.ExifInterface.dll => 0xe3114406a52f1e84 => 282
	i64 16423015068819898779, ; 655: Xamarin.Kotlin.StdLib.Jdk8 => 0xe3ea453135e5c19b => 332
	i64 16454459195343277943, ; 656: System.Net.NetworkInformation => 0xe459fb756d988f77 => 68
	i64 16496768397145114574, ; 657: Mono.Android.Export.dll => 0xe4f04b741db987ce => 169
	i64 16523284800709429098, ; 658: Microsoft.AspNetCore.DataProtection.dll => 0xe54e7ffb6ce5876a => 196
	i64 16589693266713801121, ; 659: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xe63a6e214f2a71a1 => 295
	i64 16621146507174665210, ; 660: Xamarin.AndroidX.ConstraintLayout => 0xe6aa2caf87dedbfa => 269
	i64 16649148416072044166, ; 661: Microsoft.Maui.Graphics => 0xe70da84600bb4e86 => 239
	i64 16677317093839702854, ; 662: Xamarin.AndroidX.Navigation.UI => 0xe771bb8960dd8b46 => 302
	i64 16702652415771857902, ; 663: System.ValueTuple => 0xe7cbbde0b0e6d3ee => 151
	i64 16709499819875633724, ; 664: System.IO.Compression.ZipFile => 0xe7e4118e32240a3c => 45
	i64 16737807731308835127, ; 665: System.Runtime.Intrinsics => 0xe848a3736f733137 => 108
	i64 16755018182064898362, ; 666: SQLitePCLRaw.core.dll => 0xe885c843c330813a => 243
	i64 16758309481308491337, ; 667: System.IO.FileSystem.DriveInfo => 0xe89179af15740e49 => 48
	i64 16762783179241323229, ; 668: System.Reflection.TypeExtensions => 0xe8a15e7d0d927add => 96
	i64 16763512187801783862, ; 669: Microsoft.JSInterop.WebAssembly => 0xe8a3f58495db4e36 => 233
	i64 16765015072123548030, ; 670: System.Diagnostics.TextWriterTraceListener.dll => 0xe8a94c621bfe717e => 31
	i64 16822611501064131242, ; 671: System.Data.DataSetExtensions => 0xe975ec07bb5412aa => 23
	i64 16833383113903931215, ; 672: mscorlib => 0xe99c30c1484d7f4f => 166
	i64 16856067890322379635, ; 673: System.Data.Common.dll => 0xe9ecc87060889373 => 22
	i64 16890310621557459193, ; 674: System.Text.RegularExpressions.dll => 0xea66700587f088f9 => 138
	i64 16933958494752847024, ; 675: System.Net.WebProxy.dll => 0xeb018187f0f3b4b0 => 78
	i64 16942731696432749159, ; 676: sk\Microsoft.Maui.Controls.resources => 0xeb20acb622a01a67 => 360
	i64 16977952268158210142, ; 677: System.IO.Pipes.AccessControl => 0xeb9dcda2851b905e => 54
	i64 16989020923549080504, ; 678: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xebc52084add25bb8 => 295
	i64 16998075588627545693, ; 679: Xamarin.AndroidX.Navigation.Fragment => 0xebe54bb02d623e5d => 300
	i64 17008137082415910100, ; 680: System.Collections.NonGeneric => 0xec090a90408c8cd4 => 10
	i64 17024911836938395553, ; 681: Xamarin.AndroidX.Annotation.Experimental.dll => 0xec44a31d250e5fa1 => 258
	i64 17031351772568316411, ; 682: Xamarin.AndroidX.Navigation.Common.dll => 0xec5b843380a769fb => 299
	i64 17037200463775726619, ; 683: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xec704b8e0a78fc1b => 286
	i64 17047433665992082296, ; 684: Microsoft.Extensions.Configuration.FileExtensions => 0xec94a699197e4378 => 214
	i64 17062143951396181894, ; 685: System.ComponentModel.Primitives => 0xecc8e986518c9786 => 16
	i64 17079998892748052666, ; 686: Microsoft.AspNetCore.Components.dll => 0xed08587fce5018ba => 185
	i64 17080685096938066842, ; 687: Microsoft.AspNetCore.Components.QuickGrid => 0xed0ac8992b4d539a => 188
	i64 17089008752050867324, ; 688: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xed285aeb25888c7c => 367
	i64 17118171214553292978, ; 689: System.Threading.Channels => 0xed8ff6060fc420b2 => 139
	i64 17126545051278881272, ; 690: Microsoft.Net.Http.Headers.dll => 0xedadb5fbdb33b1f8 => 240
	i64 17187273293601214786, ; 691: System.ComponentModel.Annotations.dll => 0xee8575ff9aa89142 => 13
	i64 17201328579425343169, ; 692: System.ComponentModel.EventBasedAsync => 0xeeb76534d96c16c1 => 15
	i64 17202182880784296190, ; 693: System.Security.Cryptography.Encoding.dll => 0xeeba6e30627428fe => 122
	i64 17205988430934219272, ; 694: Microsoft.Extensions.FileSystemGlobbing => 0xeec7f35113509a08 => 222
	i64 17230721278011714856, ; 695: System.Private.Xml.Linq => 0xef1fd1b5c7a72d28 => 87
	i64 17234219099804750107, ; 696: System.Transactions.Local.dll => 0xef2c3ef5e11d511b => 149
	i64 17260702271250283638, ; 697: System.Data.Common => 0xef8a5543bba6bc76 => 22
	i64 17333249706306540043, ; 698: System.Diagnostics.Tracing.dll => 0xf08c12c5bb8b920b => 34
	i64 17338386382517543202, ; 699: System.Net.WebSockets.Client.dll => 0xf09e528d5c6da122 => 79
	i64 17342750010158924305, ; 700: hi\Microsoft.Maui.Controls.resources => 0xf0add33f97ecc211 => 345
	i64 17360349973592121190, ; 701: Xamarin.Google.Crypto.Tink.Android => 0xf0ec5a52686b9f66 => 325
	i64 17438153253682247751, ; 702: sk/Microsoft.Maui.Controls.resources.dll => 0xf200c3fe308d7847 => 360
	i64 17470386307322966175, ; 703: System.Threading.Timer => 0xf27347c8d0d5709f => 147
	i64 17509662556995089465, ; 704: System.Net.WebSockets.dll => 0xf2fed1534ea67439 => 80
	i64 17514990004910432069, ; 705: fr\Microsoft.Maui.Controls.resources => 0xf311be9c6f341f45 => 343
	i64 17522591619082469157, ; 706: GoogleGson => 0xf32cc03d27a5bf25 => 177
	i64 17590473451926037903, ; 707: Xamarin.Android.Glide => 0xf41dea67fcfda58f => 251
	i64 17623389608345532001, ; 708: pl\Microsoft.Maui.Controls.resources => 0xf492db79dfbef661 => 355
	i64 17627500474728259406, ; 709: System.Globalization => 0xf4a176498a351f4e => 42
	i64 17685921127322830888, ; 710: System.Diagnostics.Debug.dll => 0xf571038fafa74828 => 26
	i64 17702523067201099846, ; 711: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xf5abfef008ae1846 => 366
	i64 17704177640604968747, ; 712: Xamarin.AndroidX.Loader => 0xf5b1dfc36cac272b => 297
	i64 17710060891934109755, ; 713: Xamarin.AndroidX.Lifecycle.ViewModel => 0xf5c6c68c9e45303b => 294
	i64 17712670374920797664, ; 714: System.Runtime.InteropServices.dll => 0xf5d00bdc38bd3de0 => 107
	i64 17777860260071588075, ; 715: System.Runtime.Numerics.dll => 0xf6b7a5b72419c0eb => 110
	i64 17838668724098252521, ; 716: System.Buffers.dll => 0xf78faeb0f5bf3ee9 => 7
	i64 17864403808740949822, ; 717: Microsoft.AspNetCore.Components.QuickGrid.dll => 0xf7eb1c9d4823bf3e => 188
	i64 17891337867145587222, ; 718: Xamarin.Jetbrains.Annotations => 0xf84accff6fb52a16 => 328
	i64 17910264068556501837, ; 719: Microsoft.Extensions.Identity.Core => 0xf88e0a4717c0b34d => 224
	i64 17911643751311784505, ; 720: Microsoft.Net.Http.Headers => 0xf892f1178448ba39 => 240
	i64 17928294245072900555, ; 721: System.IO.Compression.FileSystem.dll => 0xf8ce18a0b24011cb => 44
	i64 17981332794496557478, ; 722: EntityFramework.SqlServer => 0xf98a86e84c0ca1a6 => 175
	i64 17992315986609351877, ; 723: System.Xml.XmlDocument.dll => 0xf9b18c0ffc6eacc5 => 161
	i64 18017743553296241350, ; 724: Microsoft.Extensions.Caching.Abstractions => 0xfa0be24cb44e92c6 => 209
	i64 18025913125965088385, ; 725: System.Threading => 0xfa28e87b91334681 => 148
	i64 18099568558057551825, ; 726: nl/Microsoft.Maui.Controls.resources.dll => 0xfb2e95b53ad977d1 => 354
	i64 18116111925905154859, ; 727: Xamarin.AndroidX.Arch.Core.Runtime => 0xfb695bd036cb632b => 263
	i64 18121036031235206392, ; 728: Xamarin.AndroidX.Navigation.Common => 0xfb7ada42d3d42cf8 => 299
	i64 18146411883821974900, ; 729: System.Formats.Asn1.dll => 0xfbd50176eb22c574 => 38
	i64 18146811631844267958, ; 730: System.ComponentModel.EventBasedAsync.dll => 0xfbd66d08820117b6 => 15
	i64 18203743254473369877, ; 731: System.Security.Cryptography.Pkcs.dll => 0xfca0b00ad94c6915 => 248
	i64 18225059387460068507, ; 732: System.Threading.ThreadPool.dll => 0xfcec6af3cff4a49b => 146
	i64 18245806341561545090, ; 733: System.Collections.Concurrent.dll => 0xfd3620327d587182 => 8
	i64 18246353452277720183, ; 734: Microsoft.AspNetCore.Components.QuickGrid.EntityFrameworkAdapter.dll => 0xfd3811caa13c6477 => 189
	i64 18260797123374478311, ; 735: Xamarin.AndroidX.Emoji2 => 0xfd6b623bde35f3e7 => 280
	i64 18284618658670613420, ; 736: System.IO.Packaging.dll => 0xfdc003cb438a93ac => 246
	i64 18305135509493619199, ; 737: Xamarin.AndroidX.Navigation.Runtime.dll => 0xfe08e7c2d8c199ff => 301
	i64 18318849532986632368, ; 738: System.Security.dll => 0xfe39a097c37fa8b0 => 130
	i64 18324163916253801303, ; 739: it\Microsoft.Maui.Controls.resources => 0xfe4c81ff0a56ab57 => 349
	i64 18337789674395175250, ; 740: Microsoft.AspNetCore.Components.WebAssembly.dll => 0xfe7cea8d14268952 => 191
	i64 18341799084585866416, ; 741: DocumentFormat.OpenXml.Framework => 0xfe8b2916a25354b0 => 174
	i64 18370042311372477656, ; 742: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0xfeef80274e4094d8 => 244
	i64 18380184030268848184, ; 743: Xamarin.AndroidX.VersionedParcelable => 0xff1387fe3e7b7838 => 317
	i64 18428404840311395189, ; 744: System.Security.Cryptography.Xml.dll => 0xffbed8907bd99375 => 249
	i64 18439108438687598470 ; 745: System.Reflection.Metadata.dll => 0xffe4df6e2ee1c786 => 94
], align 16

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [746 x i32] [
	i32 279, ; 0
	i32 230, ; 1
	i32 171, ; 2
	i32 238, ; 3
	i32 223, ; 4
	i32 58, ; 5
	i32 266, ; 6
	i32 151, ; 7
	i32 307, ; 8
	i32 310, ; 9
	i32 273, ; 10
	i32 132, ; 11
	i32 56, ; 12
	i32 309, ; 13
	i32 228, ; 14
	i32 342, ; 15
	i32 95, ; 16
	i32 370, ; 17
	i32 292, ; 18
	i32 214, ; 19
	i32 129, ; 20
	i32 213, ; 21
	i32 220, ; 22
	i32 145, ; 23
	i32 267, ; 24
	i32 18, ; 25
	i32 345, ; 26
	i32 244, ; 27
	i32 278, ; 28
	i32 293, ; 29
	i32 150, ; 30
	i32 104, ; 31
	i32 183, ; 32
	i32 95, ; 33
	i32 323, ; 34
	i32 353, ; 35
	i32 371, ; 36
	i32 189, ; 37
	i32 36, ; 38
	i32 243, ; 39
	i32 28, ; 40
	i32 262, ; 41
	i32 180, ; 42
	i32 300, ; 43
	i32 50, ; 44
	i32 115, ; 45
	i32 70, ; 46
	i32 235, ; 47
	i32 65, ; 48
	i32 170, ; 49
	i32 179, ; 50
	i32 245, ; 51
	i32 145, ; 52
	i32 351, ; 53
	i32 321, ; 54
	i32 261, ; 55
	i32 296, ; 56
	i32 286, ; 57
	i32 194, ; 58
	i32 40, ; 59
	i32 89, ; 60
	i32 81, ; 61
	i32 66, ; 62
	i32 62, ; 63
	i32 86, ; 64
	i32 232, ; 65
	i32 260, ; 66
	i32 106, ; 67
	i32 341, ; 68
	i32 307, ; 69
	i32 174, ; 70
	i32 102, ; 71
	i32 35, ; 72
	i32 257, ; 73
	i32 363, ; 74
	i32 309, ; 75
	i32 236, ; 76
	i32 363, ; 77
	i32 119, ; 78
	i32 294, ; 79
	i32 337, ; 80
	i32 355, ; 81
	i32 142, ; 82
	i32 141, ; 83
	i32 331, ; 84
	i32 53, ; 85
	i32 35, ; 86
	i32 141, ; 87
	i32 254, ; 88
	i32 264, ; 89
	i32 208, ; 90
	i32 227, ; 91
	i32 173, ; 92
	i32 278, ; 93
	i32 8, ; 94
	i32 14, ; 95
	i32 193, ; 96
	i32 359, ; 97
	i32 306, ; 98
	i32 51, ; 99
	i32 289, ; 100
	i32 136, ; 101
	i32 101, ; 102
	i32 271, ; 103
	i32 316, ; 104
	i32 116, ; 105
	i32 255, ; 106
	i32 163, ; 107
	i32 362, ; 108
	i32 166, ; 109
	i32 67, ; 110
	i32 216, ; 111
	i32 337, ; 112
	i32 80, ; 113
	i32 175, ; 114
	i32 232, ; 115
	i32 101, ; 116
	i32 311, ; 117
	i32 117, ; 118
	i32 342, ; 119
	i32 324, ; 120
	i32 78, ; 121
	i32 323, ; 122
	i32 369, ; 123
	i32 114, ; 124
	i32 121, ; 125
	i32 48, ; 126
	i32 223, ; 127
	i32 184, ; 128
	i32 128, ; 129
	i32 287, ; 130
	i32 258, ; 131
	i32 82, ; 132
	i32 110, ; 133
	i32 75, ; 134
	i32 334, ; 135
	i32 218, ; 136
	i32 238, ; 137
	i32 53, ; 138
	i32 313, ; 139
	i32 211, ; 140
	i32 69, ; 141
	i32 312, ; 142
	i32 210, ; 143
	i32 83, ; 144
	i32 172, ; 145
	i32 357, ; 146
	i32 116, ; 147
	i32 212, ; 148
	i32 156, ; 149
	i32 211, ; 150
	i32 252, ; 151
	i32 167, ; 152
	i32 305, ; 153
	i32 192, ; 154
	i32 279, ; 155
	i32 205, ; 156
	i32 225, ; 157
	i32 32, ; 158
	i32 221, ; 159
	i32 236, ; 160
	i32 122, ; 161
	i32 72, ; 162
	i32 62, ; 163
	i32 197, ; 164
	i32 161, ; 165
	i32 113, ; 166
	i32 88, ; 167
	i32 234, ; 168
	i32 368, ; 169
	i32 105, ; 170
	i32 18, ; 171
	i32 146, ; 172
	i32 118, ; 173
	i32 58, ; 174
	i32 273, ; 175
	i32 17, ; 176
	i32 369, ; 177
	i32 181, ; 178
	i32 52, ; 179
	i32 202, ; 180
	i32 198, ; 181
	i32 92, ; 182
	i32 242, ; 183
	i32 365, ; 184
	i32 55, ; 185
	i32 370, ; 186
	i32 187, ; 187
	i32 129, ; 188
	i32 152, ; 189
	i32 41, ; 190
	i32 92, ; 191
	i32 208, ; 192
	i32 317, ; 193
	i32 50, ; 194
	i32 335, ; 195
	i32 162, ; 196
	i32 13, ; 197
	i32 291, ; 198
	i32 255, ; 199
	i32 312, ; 200
	i32 36, ; 201
	i32 67, ; 202
	i32 109, ; 203
	i32 256, ; 204
	i32 99, ; 205
	i32 99, ; 206
	i32 11, ; 207
	i32 180, ; 208
	i32 206, ; 209
	i32 178, ; 210
	i32 200, ; 211
	i32 11, ; 212
	i32 190, ; 213
	i32 322, ; 214
	i32 298, ; 215
	i32 25, ; 216
	i32 128, ; 217
	i32 76, ; 218
	i32 290, ; 219
	i32 109, ; 220
	i32 316, ; 221
	i32 314, ; 222
	i32 106, ; 223
	i32 2, ; 224
	i32 26, ; 225
	i32 269, ; 226
	i32 157, ; 227
	i32 361, ; 228
	i32 21, ; 229
	i32 364, ; 230
	i32 49, ; 231
	i32 43, ; 232
	i32 126, ; 233
	i32 259, ; 234
	i32 59, ; 235
	i32 183, ; 236
	i32 119, ; 237
	i32 319, ; 238
	i32 282, ; 239
	i32 268, ; 240
	i32 3, ; 241
	i32 288, ; 242
	i32 191, ; 243
	i32 308, ; 244
	i32 38, ; 245
	i32 124, ; 246
	i32 358, ; 247
	i32 308, ; 248
	i32 0, ; 249
	i32 186, ; 250
	i32 242, ; 251
	i32 358, ; 252
	i32 137, ; 253
	i32 149, ; 254
	i32 371, ; 255
	i32 85, ; 256
	i32 90, ; 257
	i32 292, ; 258
	i32 372, ; 259
	i32 289, ; 260
	i32 346, ; 261
	i32 264, ; 262
	i32 275, ; 263
	i32 320, ; 264
	i32 229, ; 265
	i32 326, ; 266
	i32 290, ; 267
	i32 133, ; 268
	i32 96, ; 269
	i32 3, ; 270
	i32 354, ; 271
	i32 105, ; 272
	i32 357, ; 273
	i32 33, ; 274
	i32 154, ; 275
	i32 158, ; 276
	i32 155, ; 277
	i32 82, ; 278
	i32 199, ; 279
	i32 192, ; 280
	i32 284, ; 281
	i32 249, ; 282
	i32 143, ; 283
	i32 87, ; 284
	i32 19, ; 285
	i32 285, ; 286
	i32 248, ; 287
	i32 51, ; 288
	i32 254, ; 289
	i32 361, ; 290
	i32 61, ; 291
	i32 54, ; 292
	i32 4, ; 293
	i32 200, ; 294
	i32 97, ; 295
	i32 253, ; 296
	i32 17, ; 297
	i32 155, ; 298
	i32 84, ; 299
	i32 29, ; 300
	i32 45, ; 301
	i32 322, ; 302
	i32 64, ; 303
	i32 66, ; 304
	i32 352, ; 305
	i32 172, ; 306
	i32 293, ; 307
	i32 1, ; 308
	i32 329, ; 309
	i32 47, ; 310
	i32 24, ; 311
	i32 261, ; 312
	i32 209, ; 313
	i32 187, ; 314
	i32 165, ; 315
	i32 108, ; 316
	i32 12, ; 317
	i32 287, ; 318
	i32 63, ; 319
	i32 27, ; 320
	i32 23, ; 321
	i32 93, ; 322
	i32 168, ; 323
	i32 12, ; 324
	i32 333, ; 325
	i32 239, ; 326
	i32 29, ; 327
	i32 103, ; 328
	i32 14, ; 329
	i32 126, ; 330
	i32 270, ; 331
	i32 231, ; 332
	i32 302, ; 333
	i32 233, ; 334
	i32 91, ; 335
	i32 291, ; 336
	i32 195, ; 337
	i32 250, ; 338
	i32 9, ; 339
	i32 86, ; 340
	i32 281, ; 341
	i32 314, ; 342
	i32 199, ; 343
	i32 356, ; 344
	i32 71, ; 345
	i32 168, ; 346
	i32 1, ; 347
	i32 301, ; 348
	i32 5, ; 349
	i32 356, ; 350
	i32 44, ; 351
	i32 27, ; 352
	i32 330, ; 353
	i32 158, ; 354
	i32 304, ; 355
	i32 112, ; 356
	i32 366, ; 357
	i32 210, ; 358
	i32 121, ; 359
	i32 207, ; 360
	i32 319, ; 361
	i32 260, ; 362
	i32 198, ; 363
	i32 159, ; 364
	i32 195, ; 365
	i32 131, ; 366
	i32 325, ; 367
	i32 57, ; 368
	i32 179, ; 369
	i32 215, ; 370
	i32 138, ; 371
	i32 83, ; 372
	i32 30, ; 373
	i32 271, ; 374
	i32 10, ; 375
	i32 321, ; 376
	i32 171, ; 377
	i32 182, ; 378
	i32 268, ; 379
	i32 150, ; 380
	i32 94, ; 381
	i32 194, ; 382
	i32 281, ; 383
	i32 176, ; 384
	i32 60, ; 385
	i32 237, ; 386
	i32 157, ; 387
	i32 341, ; 388
	i32 227, ; 389
	i32 64, ; 390
	i32 88, ; 391
	i32 79, ; 392
	i32 47, ; 393
	i32 235, ; 394
	i32 143, ; 395
	i32 338, ; 396
	i32 213, ; 397
	i32 331, ; 398
	i32 275, ; 399
	i32 74, ; 400
	i32 201, ; 401
	i32 91, ; 402
	i32 328, ; 403
	i32 135, ; 404
	i32 90, ; 405
	i32 313, ; 406
	i32 334, ; 407
	i32 272, ; 408
	i32 204, ; 409
	i32 336, ; 410
	i32 112, ; 411
	i32 42, ; 412
	i32 159, ; 413
	i32 246, ; 414
	i32 4, ; 415
	i32 103, ; 416
	i32 176, ; 417
	i32 220, ; 418
	i32 70, ; 419
	i32 60, ; 420
	i32 39, ; 421
	i32 262, ; 422
	i32 153, ; 423
	i32 56, ; 424
	i32 34, ; 425
	i32 226, ; 426
	i32 237, ; 427
	i32 259, ; 428
	i32 21, ; 429
	i32 163, ; 430
	i32 203, ; 431
	i32 219, ; 432
	i32 326, ; 433
	i32 347, ; 434
	i32 324, ; 435
	i32 318, ; 436
	i32 140, ; 437
	i32 185, ; 438
	i32 350, ; 439
	i32 229, ; 440
	i32 89, ; 441
	i32 204, ; 442
	i32 147, ; 443
	i32 274, ; 444
	i32 162, ; 445
	i32 303, ; 446
	i32 6, ; 447
	i32 169, ; 448
	i32 31, ; 449
	i32 107, ; 450
	i32 284, ; 451
	i32 247, ; 452
	i32 348, ; 453
	i32 318, ; 454
	i32 225, ; 455
	i32 257, ; 456
	i32 311, ; 457
	i32 224, ; 458
	i32 167, ; 459
	i32 285, ; 460
	i32 140, ; 461
	i32 344, ; 462
	i32 59, ; 463
	i32 241, ; 464
	i32 144, ; 465
	i32 241, ; 466
	i32 81, ; 467
	i32 74, ; 468
	i32 221, ; 469
	i32 222, ; 470
	i32 130, ; 471
	i32 25, ; 472
	i32 7, ; 473
	i32 93, ; 474
	i32 315, ; 475
	i32 137, ; 476
	i32 251, ; 477
	i32 113, ; 478
	i32 9, ; 479
	i32 245, ; 480
	i32 247, ; 481
	i32 104, ; 482
	i32 19, ; 483
	i32 181, ; 484
	i32 283, ; 485
	i32 186, ; 486
	i32 297, ; 487
	i32 372, ; 488
	i32 277, ; 489
	i32 33, ; 490
	i32 265, ; 491
	i32 46, ; 492
	i32 349, ; 493
	i32 30, ; 494
	i32 266, ; 495
	i32 57, ; 496
	i32 134, ; 497
	i32 114, ; 498
	i32 320, ; 499
	i32 362, ; 500
	i32 332, ; 501
	i32 55, ; 502
	i32 230, ; 503
	i32 6, ; 504
	i32 77, ; 505
	i32 276, ; 506
	i32 193, ; 507
	i32 111, ; 508
	i32 197, ; 509
	i32 280, ; 510
	i32 250, ; 511
	i32 102, ; 512
	i32 336, ; 513
	i32 350, ; 514
	i32 170, ; 515
	i32 115, ; 516
	i32 344, ; 517
	i32 315, ; 518
	i32 270, ; 519
	i32 201, ; 520
	i32 76, ; 521
	i32 327, ; 522
	i32 85, ; 523
	i32 329, ; 524
	i32 364, ; 525
	i32 263, ; 526
	i32 365, ; 527
	i32 348, ; 528
	i32 218, ; 529
	i32 305, ; 530
	i32 160, ; 531
	i32 2, ; 532
	i32 276, ; 533
	i32 24, ; 534
	i32 256, ; 535
	i32 32, ; 536
	i32 117, ; 537
	i32 37, ; 538
	i32 16, ; 539
	i32 343, ; 540
	i32 52, ; 541
	i32 346, ; 542
	i32 330, ; 543
	i32 20, ; 544
	i32 123, ; 545
	i32 206, ; 546
	i32 154, ; 547
	i32 283, ; 548
	i32 131, ; 549
	i32 205, ; 550
	i32 338, ; 551
	i32 265, ; 552
	i32 148, ; 553
	i32 207, ; 554
	i32 173, ; 555
	i32 252, ; 556
	i32 120, ; 557
	i32 28, ; 558
	i32 132, ; 559
	i32 100, ; 560
	i32 134, ; 561
	i32 303, ; 562
	i32 153, ; 563
	i32 97, ; 564
	i32 125, ; 565
	i32 253, ; 566
	i32 69, ; 567
	i32 72, ; 568
	i32 359, ; 569
	i32 288, ; 570
	i32 306, ; 571
	i32 340, ; 572
	i32 136, ; 573
	i32 124, ; 574
	i32 71, ; 575
	i32 111, ; 576
	i32 298, ; 577
	i32 216, ; 578
	i32 152, ; 579
	i32 351, ; 580
	i32 367, ; 581
	i32 327, ; 582
	i32 118, ; 583
	i32 274, ; 584
	i32 177, ; 585
	i32 368, ; 586
	i32 0, ; 587
	i32 335, ; 588
	i32 127, ; 589
	i32 133, ; 590
	i32 217, ; 591
	i32 77, ; 592
	i32 46, ; 593
	i32 277, ; 594
	i32 73, ; 595
	i32 63, ; 596
	i32 98, ; 597
	i32 84, ; 598
	i32 352, ; 599
	i32 43, ; 600
	i32 61, ; 601
	i32 304, ; 602
	i32 212, ; 603
	i32 37, ; 604
	i32 40, ; 605
	i32 267, ; 606
	i32 333, ; 607
	i32 160, ; 608
	i32 98, ; 609
	i32 178, ; 610
	i32 272, ; 611
	i32 217, ; 612
	i32 190, ; 613
	i32 219, ; 614
	i32 135, ; 615
	i32 20, ; 616
	i32 65, ; 617
	i32 339, ; 618
	i32 125, ; 619
	i32 75, ; 620
	i32 202, ; 621
	i32 296, ; 622
	i32 184, ; 623
	i32 164, ; 624
	i32 156, ; 625
	i32 339, ; 626
	i32 5, ; 627
	i32 347, ; 628
	i32 49, ; 629
	i32 310, ; 630
	i32 340, ; 631
	i32 144, ; 632
	i32 215, ; 633
	i32 139, ; 634
	i32 203, ; 635
	i32 100, ; 636
	i32 182, ; 637
	i32 234, ; 638
	i32 123, ; 639
	i32 120, ; 640
	i32 142, ; 641
	i32 39, ; 642
	i32 68, ; 643
	i32 196, ; 644
	i32 228, ; 645
	i32 41, ; 646
	i32 231, ; 647
	i32 164, ; 648
	i32 73, ; 649
	i32 353, ; 650
	i32 165, ; 651
	i32 226, ; 652
	i32 127, ; 653
	i32 282, ; 654
	i32 332, ; 655
	i32 68, ; 656
	i32 169, ; 657
	i32 196, ; 658
	i32 295, ; 659
	i32 269, ; 660
	i32 239, ; 661
	i32 302, ; 662
	i32 151, ; 663
	i32 45, ; 664
	i32 108, ; 665
	i32 243, ; 666
	i32 48, ; 667
	i32 96, ; 668
	i32 233, ; 669
	i32 31, ; 670
	i32 23, ; 671
	i32 166, ; 672
	i32 22, ; 673
	i32 138, ; 674
	i32 78, ; 675
	i32 360, ; 676
	i32 54, ; 677
	i32 295, ; 678
	i32 300, ; 679
	i32 10, ; 680
	i32 258, ; 681
	i32 299, ; 682
	i32 286, ; 683
	i32 214, ; 684
	i32 16, ; 685
	i32 185, ; 686
	i32 188, ; 687
	i32 367, ; 688
	i32 139, ; 689
	i32 240, ; 690
	i32 13, ; 691
	i32 15, ; 692
	i32 122, ; 693
	i32 222, ; 694
	i32 87, ; 695
	i32 149, ; 696
	i32 22, ; 697
	i32 34, ; 698
	i32 79, ; 699
	i32 345, ; 700
	i32 325, ; 701
	i32 360, ; 702
	i32 147, ; 703
	i32 80, ; 704
	i32 343, ; 705
	i32 177, ; 706
	i32 251, ; 707
	i32 355, ; 708
	i32 42, ; 709
	i32 26, ; 710
	i32 366, ; 711
	i32 297, ; 712
	i32 294, ; 713
	i32 107, ; 714
	i32 110, ; 715
	i32 7, ; 716
	i32 188, ; 717
	i32 328, ; 718
	i32 224, ; 719
	i32 240, ; 720
	i32 44, ; 721
	i32 175, ; 722
	i32 161, ; 723
	i32 209, ; 724
	i32 148, ; 725
	i32 354, ; 726
	i32 263, ; 727
	i32 299, ; 728
	i32 38, ; 729
	i32 15, ; 730
	i32 248, ; 731
	i32 146, ; 732
	i32 8, ; 733
	i32 189, ; 734
	i32 280, ; 735
	i32 246, ; 736
	i32 301, ; 737
	i32 130, ; 738
	i32 349, ; 739
	i32 191, ; 740
	i32 174, ; 741
	i32 244, ; 742
	i32 317, ; 743
	i32 249, ; 744
	i32 94 ; 745
], align 16

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 8

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 8

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 8

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 8, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 16

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ 82d8938cf80f6d5fa6c28529ddfbdb753d805ab4"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
