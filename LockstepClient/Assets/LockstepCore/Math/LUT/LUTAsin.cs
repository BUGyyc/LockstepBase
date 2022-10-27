﻿using System;

namespace Lockstep
{
    public static class LUTAsin
    {
        public static readonly int COUNT;
        public static readonly int HALF_COUNT;
        public static readonly int[] table;
        static LUTAsin()
        {
            COUNT = 1024;
            HALF_COUNT = COUNT >> 1;
            table = new int[]
            {
                -15707,
                -15082,
                -14823,
                -14624,
                -14457,
                -14309,
                -14175,
                -14052,
                -13937,
                -13830,
                -13728,
                -13631,
                -13538,
                -13449,
                -13364,
                -13281,
                -13201,
                -13123,
                -13048,
                -12975,
                -12903,
                -12833,
                -12765,
                -12699,
                -12634,
                -12570,
                -12507,
                -12445,
                -12385,
                -12326,
                -12267,
                -12210,
                -12153,
                -12098,
                -12043,
                -11989,
                -11935,
                -11882,
                -11830,
                -11779,
                -11728,
                -11678,
                -11629,
                -11580,
                -11531,
                -11484,
                -11436,
                -11389,
                -11343,
                -11297,
                -11251,
                -11206,
                -11161,
                -11117,
                -11073,
                -11030,
                -10987,
                -10944,
                -10901,
                -10859,
                -10818,
                -10776,
                -10735,
                -10694,
                -10654,
                -10614,
                -10574,
                -10534,
                -10495,
                -10456,
                -10417,
                -10378,
                -10340,
                -10302,
                -10264,
                -10226,
                -10189,
                -10152,
                -10115,
                -10078,
                -10042,
                -10006,
                -9969,
                -9934,
                -9898,
                -9862,
                -9827,
                -9792,
                -9757,
                -9722,
                -9688,
                -9653,
                -9619,
                -9585,
                -9551,
                -9517,
                -9484,
                -9450,
                -9417,
                -9384,
                -9351,
                -9318,
                -9285,
                -9253,
                -9221,
                -9188,
                -9156,
                -9124,
                -9092,
                -9061,
                -9029,
                -8998,
                -8966,
                -8935,
                -8904,
                -8873,
                -8842,
                -8811,
                -8781,
                -8750,
                -8720,
                -8689,
                -8659,
                -8629,
                -8599,
                -8569,
                -8539,
                -8510,
                -8480,
                -8451,
                -8421,
                -8392,
                -8363,
                -8334,
                -8305,
                -8276,
                -8247,
                -8218,
                -8190,
                -8161,
                -8133,
                -8104,
                -8076,
                -8048,
                -8020,
                -7991,
                -7964,
                -7936,
                -7908,
                -7880,
                -7852,
                -7825,
                -7797,
                -7770,
                -7743,
                -7715,
                -7688,
                -7661,
                -7634,
                -7607,
                -7580,
                -7553,
                -7526,
                -7500,
                -7473,
                -7446,
                -7420,
                -7393,
                -7367,
                -7341,
                -7314,
                -7288,
                -7262,
                -7236,
                -7210,
                -7184,
                -7158,
                -7132,
                -7106,
                -7080,
                -7055,
                -7029,
                -7004,
                -6978,
                -6953,
                -6927,
                -6902,
                -6877,
                -6851,
                -6826,
                -6801,
                -6776,
                -6751,
                -6726,
                -6701,
                -6676,
                -6651,
                -6626,
                -6602,
                -6577,
                -6552,
                -6528,
                -6503,
                -6479,
                -6454,
                -6430,
                -6405,
                -6381,
                -6357,
                -6332,
                -6308,
                -6284,
                -6260,
                -6236,
                -6212,
                -6188,
                -6164,
                -6140,
                -6116,
                -6092,
                -6068,
                -6045,
                -6021,
                -5997,
                -5974,
                -5950,
                -5926,
                -5903,
                -5879,
                -5856,
                -5832,
                -5809,
                -5786,
                -5762,
                -5739,
                -5716,
                -5693,
                -5670,
                -5646,
                -5623,
                -5600,
                -5577,
                -5554,
                -5531,
                -5508,
                -5485,
                -5463,
                -5440,
                -5417,
                -5394,
                -5371,
                -5349,
                -5326,
                -5303,
                -5281,
                -5258,
                -5235,
                -5213,
                -5190,
                -5168,
                -5146,
                -5123,
                -5101,
                -5078,
                -5056,
                -5034,
                -5011,
                -4989,
                -4967,
                -4945,
                -4923,
                -4900,
                -4878,
                -4856,
                -4834,
                -4812,
                -4790,
                -4768,
                -4746,
                -4724,
                -4702,
                -4680,
                -4658,
                -4637,
                -4615,
                -4593,
                -4571,
                -4549,
                -4528,
                -4506,
                -4484,
                -4463,
                -4441,
                -4419,
                -4398,
                -4376,
                -4355,
                -4333,
                -4312,
                -4290,
                -4269,
                -4247,
                -4226,
                -4204,
                -4183,
                -4162,
                -4140,
                -4119,
                -4098,
                -4076,
                -4055,
                -4034,
                -4013,
                -3991,
                -3970,
                -3949,
                -3928,
                -3907,
                -3886,
                -3865,
                -3843,
                -3822,
                -3801,
                -3780,
                -3759,
                -3738,
                -3717,
                -3696,
                -3675,
                -3655,
                -3634,
                -3613,
                -3592,
                -3571,
                -3550,
                -3529,
                -3509,
                -3488,
                -3467,
                -3446,
                -3426,
                -3405,
                -3384,
                -3363,
                -3343,
                -3322,
                -3301,
                -3281,
                -3260,
                -3239,
                -3219,
                -3198,
                -3178,
                -3157,
                -3137,
                -3116,
                -3096,
                -3075,
                -3055,
                -3034,
                -3014,
                -2993,
                -2973,
                -2952,
                -2932,
                -2912,
                -2891,
                -2871,
                -2850,
                -2830,
                -2810,
                -2789,
                -2769,
                -2749,
                -2729,
                -2708,
                -2688,
                -2668,
                -2648,
                -2627,
                -2607,
                -2587,
                -2567,
                -2546,
                -2526,
                -2506,
                -2486,
                -2466,
                -2446,
                -2426,
                -2405,
                -2385,
                -2365,
                -2345,
                -2325,
                -2305,
                -2285,
                -2265,
                -2245,
                -2225,
                -2205,
                -2185,
                -2165,
                -2145,
                -2125,
                -2105,
                -2085,
                -2065,
                -2045,
                -2025,
                -2005,
                -1985,
                -1965,
                -1945,
                -1925,
                -1906,
                -1886,
                -1866,
                -1846,
                -1826,
                -1806,
                -1786,
                -1766,
                -1747,
                -1727,
                -1707,
                -1687,
                -1667,
                -1648,
                -1628,
                -1608,
                -1588,
                -1568,
                -1549,
                -1529,
                -1509,
                -1489,
                -1470,
                -1450,
                -1430,
                -1410,
                -1391,
                -1371,
                -1351,
                -1332,
                -1312,
                -1292,
                -1272,
                -1253,
                -1233,
                -1213,
                -1194,
                -1174,
                -1154,
                -1135,
                -1115,
                -1095,
                -1076,
                -1056,
                -1037,
                -1017,
                -997,
                -978,
                -958,
                -938,
                -919,
                -899,
                -880,
                -860,
                -840,
                -821,
                -801,
                -782,
                -762,
                -742,
                -723,
                -703,
                -684,
                -664,
                -644,
                -625,
                -605,
                -586,
                -566,
                -547,
                -527,
                -508,
                -488,
                -468,
                -449,
                -429,
                -410,
                -390,
                -371,
                -351,
                -332,
                -312,
                -293,
                -273,
                -253,
                -234,
                -214,
                -195,
                -175,
                -156,
                -136,
                -117,
                -97,
                -78,
                -58,
                -39,
                -19,
                0,
                19,
                39,
                58,
                78,
                97,
                117,
                136,
                156,
                175,
                195,
                214,
                234,
                253,
                273,
                293,
                312,
                332,
                351,
                371,
                390,
                410,
                429,
                449,
                468,
                488,
                508,
                527,
                547,
                566,
                586,
                605,
                625,
                644,
                664,
                684,
                703,
                723,
                742,
                762,
                782,
                801,
                821,
                840,
                860,
                880,
                899,
                919,
                938,
                958,
                978,
                997,
                1017,
                1037,
                1056,
                1076,
                1095,
                1115,
                1135,
                1154,
                1174,
                1194,
                1213,
                1233,
                1253,
                1272,
                1292,
                1312,
                1332,
                1351,
                1371,
                1391,
                1410,
                1430,
                1450,
                1470,
                1489,
                1509,
                1529,
                1549,
                1568,
                1588,
                1608,
                1628,
                1648,
                1667,
                1687,
                1707,
                1727,
                1747,
                1766,
                1786,
                1806,
                1826,
                1846,
                1866,
                1886,
                1906,
                1925,
                1945,
                1965,
                1985,
                2005,
                2025,
                2045,
                2065,
                2085,
                2105,
                2125,
                2145,
                2165,
                2185,
                2205,
                2225,
                2245,
                2265,
                2285,
                2305,
                2325,
                2345,
                2365,
                2385,
                2405,
                2426,
                2446,
                2466,
                2486,
                2506,
                2526,
                2546,
                2567,
                2587,
                2607,
                2627,
                2648,
                2668,
                2688,
                2708,
                2729,
                2749,
                2769,
                2789,
                2810,
                2830,
                2850,
                2871,
                2891,
                2912,
                2932,
                2952,
                2973,
                2993,
                3014,
                3034,
                3055,
                3075,
                3096,
                3116,
                3137,
                3157,
                3178,
                3198,
                3219,
                3239,
                3260,
                3281,
                3301,
                3322,
                3343,
                3363,
                3384,
                3405,
                3426,
                3446,
                3467,
                3488,
                3509,
                3529,
                3550,
                3571,
                3592,
                3613,
                3634,
                3655,
                3675,
                3696,
                3717,
                3738,
                3759,
                3780,
                3801,
                3822,
                3843,
                3865,
                3886,
                3907,
                3928,
                3949,
                3970,
                3991,
                4013,
                4034,
                4055,
                4076,
                4098,
                4119,
                4140,
                4162,
                4183,
                4204,
                4226,
                4247,
                4269,
                4290,
                4312,
                4333,
                4355,
                4376,
                4398,
                4419,
                4441,
                4463,
                4484,
                4506,
                4528,
                4549,
                4571,
                4593,
                4615,
                4637,
                4658,
                4680,
                4702,
                4724,
                4746,
                4768,
                4790,
                4812,
                4834,
                4856,
                4878,
                4900,
                4923,
                4945,
                4967,
                4989,
                5011,
                5034,
                5056,
                5078,
                5101,
                5123,
                5146,
                5168,
                5190,
                5213,
                5235,
                5258,
                5281,
                5303,
                5326,
                5349,
                5371,
                5394,
                5417,
                5440,
                5463,
                5485,
                5508,
                5531,
                5554,
                5577,
                5600,
                5623,
                5646,
                5670,
                5693,
                5716,
                5739,
                5762,
                5786,
                5809,
                5832,
                5856,
                5879,
                5903,
                5926,
                5950,
                5974,
                5997,
                6021,
                6045,
                6068,
                6092,
                6116,
                6140,
                6164,
                6188,
                6212,
                6236,
                6260,
                6284,
                6308,
                6332,
                6357,
                6381,
                6405,
                6430,
                6454,
                6479,
                6503,
                6528,
                6552,
                6577,
                6602,
                6626,
                6651,
                6676,
                6701,
                6726,
                6751,
                6776,
                6801,
                6826,
                6851,
                6877,
                6902,
                6927,
                6953,
                6978,
                7004,
                7029,
                7055,
                7080,
                7106,
                7132,
                7158,
                7184,
                7210,
                7236,
                7262,
                7288,
                7314,
                7341,
                7367,
                7393,
                7420,
                7446,
                7473,
                7500,
                7526,
                7553,
                7580,
                7607,
                7634,
                7661,
                7688,
                7715,
                7743,
                7770,
                7797,
                7825,
                7852,
                7880,
                7908,
                7936,
                7964,
                7991,
                8020,
                8048,
                8076,
                8104,
                8133,
                8161,
                8190,
                8218,
                8247,
                8276,
                8305,
                8334,
                8363,
                8392,
                8421,
                8451,
                8480,
                8510,
                8539,
                8569,
                8599,
                8629,
                8659,
                8689,
                8720,
                8750,
                8781,
                8811,
                8842,
                8873,
                8904,
                8935,
                8966,
                8998,
                9029,
                9061,
                9092,
                9124,
                9156,
                9188,
                9221,
                9253,
                9285,
                9318,
                9351,
                9384,
                9417,
                9450,
                9484,
                9517,
                9551,
                9585,
                9619,
                9653,
                9688,
                9722,
                9757,
                9792,
                9827,
                9862,
                9898,
                9934,
                9969,
                10006,
                10042,
                10078,
                10115,
                10152,
                10189,
                10226,
                10264,
                10302,
                10340,
                10378,
                10417,
                10456,
                10495,
                10534,
                10574,
                10614,
                10654,
                10694,
                10735,
                10776,
                10818,
                10859,
                10901,
                10944,
                10987,
                11030,
                11073,
                11117,
                11161,
                11206,
                11251,
                11297,
                11343,
                11389,
                11436,
                11484,
                11531,
                11580,
                11629,
                11678,
                11728,
                11779,
                11830,
                11882,
                11935,
                11989,
                12043,
                12098,
                12153,
                12210,
                12267,
                12326,
                12385,
                12445,
                12507,
                12570,
                12634,
                12699,
                12765,
                12833,
                12903,
                12975,
                13048,
                13123,
                13201,
                13281,
                13364,
                13449,
                13538,
                13631,
                13728,
                13830,
                13937,
                14052,
                14175,
                14309,
                14457,
                14624,
                14823,
                15082,
                15707
            };
        }
    }
}