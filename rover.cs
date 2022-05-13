using pygame;
using random;
using sys;
using os;
using time;
using System.Collections.Generic;
using System;

namespace Rover {

    /// <summary>
    /// Initial: https://github.com/xpd54/Car-race-game
    /// </summary>
    public static class Module {
        
        public static object WINDOWWIDTH = 800;
        
        public static object WINDOWHEIGHT = 600;
        
        public static object TEXTCOLOR = (255, 255, 255);
        
        public static object BACKGROUNDCOLOR = (0, 0, 0);
        
        public static object FPS = 40;
        
        public static object BADDIEMINSIZE = 10;
        
        public static object BADDIEMAXSIZE = 40;
        
        public static object BADDIEMINSPEED = 8;
        
        public static object BADDIEMAXSPEED = 8;
        
        public static object ADDNEWBADDIERATE = 6;
        
        public static object PLAYERMOVERATE = 5;
        
        public static object count = 3;
        
        public static object terminate() {
            pygame.quit();
            sys.exit();
        }
        
        public static object waitForPlayerToPressKey() {
            while (true) {
                foreach (var @event in pygame.@event.get()) {
                    if (@event.type == QUIT) {
                        terminate();
                    }
                    if (@event.type == KEYDOWN) {
                        if (@event.key == K_ESCAPE) {
                            //escape quits
                            terminate();
                        }
                        return;
                    }
                }
            }
        }
        
        public static object playerHasHitBaddie(object playerRect, object baddies) {
            foreach (var b in baddies) {
                if (playerRect.colliderect(b["rect"])) {
                    return true;
                }
            }
            return false;
        }
        
        public static object drawText(
            object text,
            object font,
            object surface,
            object x,
            object y) {
            var textobj = font.render(text, 1, TEXTCOLOR);
            var textrect = textobj.get_rect();
            textrect.topleft = (x, y);
            surface.blit(textobj, textrect);
        }
        
        static Module() {
            pygame.init();
            pygame.display.set_caption("car race");
            pygame.mouse.set_visible(false);
            pygame.mixer.music.load("music/car.wav");
            drawText("Press any key to start the game.", font, windowSurface, WINDOWWIDTH / 3 - 30, WINDOWHEIGHT / 3);
            drawText("And Enjoy", font, windowSurface, WINDOWWIDTH / 3, WINDOWHEIGHT / 3 + 30);
            pygame.display.update();
            waitForPlayerToPressKey();
            f.write(zero.ToString());
            f.close();
            v.close();
            playerRect.topleft = (WINDOWWIDTH / 2, WINDOWHEIGHT - 50);
            pygame.mixer.music.play(-1, 0.0);
            terminate();
            terminate();
            baddies.append(newBaddie);
            baddies.append(sideLeft);
            baddies.append(sideRight);
            playerRect.move_ip(-1 * PLAYERMOVERATE, 0);
            playerRect.move_ip(PLAYERMOVERATE, 0);
            playerRect.move_ip(0, -1 * PLAYERMOVERATE);
            playerRect.move_ip(0, PLAYERMOVERATE);
            b["rect"].move_ip(0, b["speed"]);
            b["rect"].move_ip(0, -5);
            b["rect"].move_ip(0, 1);
            baddies.remove(b);
            windowSurface.fill(BACKGROUNDCOLOR);
            drawText(String.Format("Score: %s", score), font, windowSurface, 128, 0);
            drawText(String.Format("Top Score: %s", topScore), font, windowSurface, 128, 20);
            drawText(String.Format("Rest Life: %s", count), font, windowSurface, 128, 40);
            windowSurface.blit(playerImage, playerRect);
            windowSurface.blit(b["surface"], b["rect"]);
            pygame.display.update();
            g.write(score.ToString());
            g.close();
            mainClock.tick(FPS);
            pygame.mixer.music.stop();
            gameOverSound.play();
            time.sleep(1);
            laugh.play();
            drawText("Game over", font, windowSurface, WINDOWWIDTH / 3, WINDOWHEIGHT / 3);
            drawText("Press any key to play again.", font, windowSurface, WINDOWWIDTH / 3 - 80, WINDOWHEIGHT / 3 + 30);
            pygame.display.update();
            time.sleep(2);
            waitForPlayerToPressKey();
            gameOverSound.stop();
        }
        
        public static object mainClock = pygame.time.Clock();
        
        public static object windowSurface = pygame.display.set_mode((WINDOWWIDTH, WINDOWHEIGHT));
        
        public static object font = pygame.font.SysFont(null, 30);
        
        public static object gameOverSound = pygame.mixer.Sound("music/crash.wav");
        
        public static object laugh = pygame.mixer.Sound("music/laugh.wav");
        
        public static object playerImage = pygame.image.load("image/car1.png");
        
        public static object car3 = pygame.image.load("image/car3.png");
        
        public static object car4 = pygame.image.load("image/car4.png");
        
        public static object playerRect = playerImage.get_rect();
        
        public static object baddieImage = pygame.image.load("image/car2.png");
        
        public static object sample = new List<object> {
            car3,
            car4,
            baddieImage
        };
        
        public static object wallLeft = pygame.image.load("image/left.png");
        
        public static object wallRight = pygame.image.load("image/right.png");
        
        public static object zero = 0;
        
        public static object f = open("data/save.dat", "w");
        
        public static object v = open("data/save.dat", "r");
        
        public static object topScore = Convert.ToInt32(v.readline());
        
        public static object baddies = new List<object>();
        
        public static object score = 0;
        
        public static object moveLeft = false;
        
        public static object reverseCheat = false;
        
        public static object baddieAddCounter = 0;
        
        public static object score = 1;
        
        public static object reverseCheat = true;
        
        public static object slowCheat = true;
        
        public static object moveRight = false;
        
        public static object moveLeft = true;
        
        public static object moveLeft = false;
        
        public static object moveRight = true;
        
        public static object moveDown = false;
        
        public static object moveUp = true;
        
        public static object moveUp = false;
        
        public static object moveDown = true;
        
        public static object reverseCheat = false;
        
        public static object score = 0;
        
        public static object slowCheat = false;
        
        public static object score = 0;
        
        public static object moveLeft = false;
        
        public static object moveRight = false;
        
        public static object moveUp = false;
        
        public static object moveDown = false;
        
        public static object baddieAddCounter = 1;
        
        public static object baddieAddCounter = 0;
        
        public static object baddieSize = 30;
        
        public static object newBaddie = new Dictionary<object, object> {
            {
                "rect",
                pygame.Rect(random.randint(140, 485), 0 - baddieSize, 23, 47)},
            {
                "speed",
                random.randint(BADDIEMINSPEED, BADDIEMAXSPEED)},
            {
                "surface",
                pygame.transform.scale(random.choice(sample), (23, 47))}};
        
        public static object sideLeft = new Dictionary<object, object> {
            {
                "rect",
                pygame.Rect(0, 0, 126, 600)},
            {
                "speed",
                random.randint(BADDIEMINSPEED, BADDIEMAXSPEED)},
            {
                "surface",
                pygame.transform.scale(wallLeft, (126, 599))}};
        
        public static object sideRight = new Dictionary<object, object> {
            {
                "rect",
                pygame.Rect(497, 0, 303, 600)},
            {
                "speed",
                random.randint(BADDIEMINSPEED, BADDIEMAXSPEED)},
            {
                "surface",
                pygame.transform.scale(wallRight, (303, 599))}};
        
        public static object g = open("data/save.dat", "w");
        
        public static object topScore = score;
        
        public static object count = count - 1;
        
        public static object count = 3;
    }
}
