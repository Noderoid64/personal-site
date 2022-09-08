import {Component, HostListener, OnInit} from '@angular/core';
import {
  Scene,
  PerspectiveCamera,
  WebGLRenderer,
  TorusGeometry,
  SphereGeometry,
  MeshBasicMaterial,
  MeshStandardMaterial,
  PointLight,
  AmbientLight,
  TextureLoader,
  Mesh,
  Clock,
  GridHelper,
  MathUtils,
} from 'three';

import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls';

@Component({
  selector: 'app-cv',
  templateUrl: './cv.component.html',
  styleUrls: ['./cv.component.scss']
})
export class CvComponent implements OnInit {

  private scene?: Scene;
  private camera?: PerspectiveCamera;
  private renderer?: WebGLRenderer;
  private pointLight?: PointLight;
  private ambientLight?: AmbientLight;
  private controls?: OrbitControls;
  private clock?: Clock;

  // private gridHelper?: GridHelper;
  private torus?: Mesh;
  private sun?: Mesh;
  private mars?: Mesh;
  private uranus?: Mesh;

  private state = 0;

  constructor() {
    document.body.onscroll = () => console.log(1);
  }


  public ngOnInit(): void {

    window.addEventListener('resize', () =>
    {

      this.camera!.aspect = window.innerWidth / window.innerHeight
      this.camera!.updateProjectionMatrix()

      // Update renderer
      this.renderer!.setSize(window.innerWidth, window.innerHeight)
      this.renderer!.setPixelRatio(Math.min(window.devicePixelRatio, 2))
    })

    this.scene = new Scene();

    this.camera = new PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
    this.camera.position.setZ(140);
    this.camera.position.setY(60);

    this.renderer = new WebGLRenderer({
      canvas: document.getElementById('bg')!,
    });
    this.renderer.setPixelRatio(window.devicePixelRatio);
    this.renderer.setSize(window.innerWidth, window.innerHeight);

    this.pointLight = new PointLight(0xffffff);
    this.pointLight.position.set(0,0,0);

    this.ambientLight = new AmbientLight(0x888888);
    this.controls = new OrbitControls(this.camera, this.renderer.domElement);
    this.clock = new Clock();
    this.clock?.start();


    const geometry = new TorusGeometry(10,3,16,100);
    const material = new MeshStandardMaterial({
      color: '#33ffdd',
      wireframe: false,
    });
    this.torus = new Mesh(geometry, material);

    const sunTexture = new TextureLoader().load('./assets/sun-texture.png')
    this.sun = new Mesh(
      new SphereGeometry(10),
      new MeshBasicMaterial({map: sunTexture})
    );

    const marsTexture = new TextureLoader().load('./assets/mars-texture.png');
    this.mars = new Mesh(
      new SphereGeometry(4),
      new MeshStandardMaterial({map: marsTexture})
    );
    this.mars.position.set(50, 0, 50);

    const uranusTexture = new TextureLoader().load('./assets/uranus-texture.jpg');
    this.uranus = new Mesh(
      new SphereGeometry(6),
      new MeshStandardMaterial({map: uranusTexture})
    );

    // this.gridHelper = new GridHelper(200,50);

    this.scene.add(this.sun);
    this.scene.add(this.mars);
    this.scene.add(this.uranus);
    // this.scene.add(this.gridHelper);
    // this.scene.add(this.torus);
    this.scene.add(this.pointLight);
    this.scene.add(this.ambientLight);
    Array(200).fill(0).forEach(this.addStar.bind(this));

    this.animate();

  }

  public animate(): void {
    requestAnimationFrame(this.animate.bind(this));
    const elapsed = this.clock?.getElapsedTime();

    if (this.sun) {
      this.sun.rotation.y += 0.0001;
    }

    if (this.mars) {
      this.mars.rotation.y += 0.0005;
      this.mars.position.x = 90 * Math.sin( 3 * 2 * Math.PI * elapsed! / 360);
      this.mars.position.z = 90 * Math.cos( 3 * 2 * Math.PI * elapsed! / 360);
    }

    if (this.uranus) {
      this.uranus.rotation.y += 0.0004;
      this.uranus.position.x = 110 * Math.sin( 2 * 2 * Math.PI * elapsed! / 360 + 2);
      this.uranus.position.z = 110 * Math.cos( 2 * 2 * Math.PI * elapsed! / 360 + 2);
    }



    this.renderer?.render(this.scene!, this.camera!);
  }

  public moveCamera(): void {
    console.log('1');
  }

  private addStar(): void {
    const g = new SphereGeometry(0.5, 24, 24);
    const m = new MeshStandardMaterial({color: 0xffffff});
    const s = new Mesh(g, m);

    var d, x, y, z;
    do {
      x = MathUtils.randFloatSpread(500) * 2.0 - 1.0;
      y = MathUtils.randFloatSpread(500) * 2.0 - 1.0;
      z = MathUtils.randFloatSpread(500) * 2.0 - 1.0;
      d = x*x + y*y + z*z;
    } while(d < 18000 || d > 25000);

    s.position.set(x,y, z);
    this.scene?.add(s);
  }

}
