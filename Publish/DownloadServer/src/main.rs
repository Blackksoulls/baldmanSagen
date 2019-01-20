extern crate reqwest;
#[macro_use] extern crate nickel;
extern crate serde;
extern crate serde_json;
extern crate chrono;
extern crate clap;

use nickel::{Nickel,HttpRouter};
use serde::{Serialize, Deserialize};
use chrono::Local;
use clap::{Arg, App};
use std::path::{Path, PathBuf};
use std::fs::{read_to_string, write, File, remove_file};

static mut VERBOSE: bool = false;

#[derive(Serialize, Deserialize, Debug)]
struct Cache {
    time: i64,
    cache: Vec<UsefullCache>
}
#[derive(Serialize, Deserialize, Debug)]
struct UsefullCache {
    tag: String,
    name: String,
    zip: String,
}
#[derive(Serialize, Deserialize, Debug, Clone)]
struct Author {
    login: String,
    id: u64,
    avatar_url: String,
    gravatar_id: String,
    url: String,
    html_url: String,
    followers_url: String,
    following_url: String,
    gists_url: String,
    starred_url: String,
    subscriptions_url: String,
    organizations_url: String,
    repos_url: String,
    events_url: String,
    received_events_url: String,
    r#type: String,
    site_admin: bool
}
#[derive(Serialize, Deserialize, Debug, Clone)]
struct Releases {
    url: String,
    assets_url: String,
    upload_url: String,
    html_url: String,
    id: u64,
    node_id: String,
    tag_name: String,
    target_commitish: String,
    name: String,
    draft: bool,
    author: Author,
    prerelease: bool,
    created_at: String,
    published_at: String,
    assets: Vec<String>,
    tarball_url: String,
    zipball_url: String,
    body: String
}

// Getting and parsing the github api
fn get_req() -> Result<Vec<Releases>, Box<std::error::Error>> {
    let req: String = reqwest::get("https://api.github.com/repos/joxcat/baldmanSagen/releases")?.text()?;
    Ok(serde_json::from_str(req.as_str())?)
}

// Getting the cache or api
fn get_release(f: PathBuf) -> Result<Cache, Box<std::error::Error>> {
    let mut result: Cache = Cache {time:0,cache:Vec::new()};
    // Creating the local timestamp
    let time = Local::now().timestamp();
    if f.exists() {
        // Getting the file content
        let content = read_to_string(f.clone())?;
        // Serializing it
        let json: Cache = match serde_json::from_str(content.as_str()) {
            Ok(x) => x,
            Err(_) => {
                let up = "{\"time\":0,\"cache\":[]}";
                write(f.clone(), up)?;
                serde_json::from_str(up)?
            }
        };
        // Si "json.time" n'est pas à la valeur par défault
        if json.time != 0 {
            // Si plus d'une heure s'est écoulée => actualisation du cache
            if (time - json.time) > 3600 {
                debug("Cache updated from api");
                // Req on github api
                let usable_req = get_req()?;
                // Result <= from github
                result.time = time;
                let mut cnt: usize = 0;
                for release in usable_req {
                    result.cache.insert(cnt, UsefullCache{tag:release.tag_name,name:release.name,zip:release.zipball_url});
                    cnt += 1;
                }
                // Creating the cache
                let up = serde_json::to_string(&result)?;
                write(f, up)?;
            } else {
                debug("Result built from cache");
                // Result <= from cache
                result = json;
            }
        } else {
            debug("Cache from api, time = default");
            // Req on github api
            let usable_req = get_req()?;
            // Result <= from github
            result.time = time;
            let mut cnt: usize = 0;
            for release in usable_req {
                result.cache.insert(cnt, UsefullCache{tag:release.tag_name,name:release.name,zip:release.zipball_url});
                cnt += 1;
            }
            // Creating the cache
            let up = serde_json::to_string(&result)?;
            write(f, up)?;
        }
    } else {
        debug("Cache file not found");
        // Req on github api
        File::create(f.clone())?;
        let up = "{\"time\":0,\"cache\":[]}";
        write(f.clone(), up)?;

        let usable_req = get_req()?;
        // Result <= from github
        result.time = time;
        let mut cnt: usize = 0;
        for release in usable_req {
            result.cache.insert(cnt, UsefullCache{tag:release.tag_name,name:release.name,zip:release.zipball_url});
            cnt += 1;
        }
    }
    Ok(result)
}

fn debug(message: &str) {
    unsafe {
        if VERBOSE {
            println!("\u{001b}[31m{}\u{001b}[0m", message);
        }
    }
}

fn main() -> Result<(), Box<std::error::Error>> {
    let matches = App::new("BaldmanSagen download server")
        .version("1.0")
        .author("Johan Planchon <dev@planchon.xyz>")
        .arg(Arg::with_name("verbose")
            .short("v")
            .long("verbose")
            .help("Active debug or not"))
        .arg(Arg::with_name("regenerate")
            .short("r")
            .long("regenerate")
            .help("Regenerate the cache"))
        .arg(Arg::with_name("port")
            .short("p")
            .long("port")
            .required(true)
            .value_name("PORT")
            .takes_value(true)
            .help("Server port"))
        .arg(Arg::with_name("address")
            .short("a")
            .long("address")
            .default_value("127.0.0.1")
            .value_name("ADDRESS")
            .takes_value(true)
            .help("Server address"))
        .get_matches();
    unsafe {
        match matches.occurrences_of("verbose") {
            0 => (),
            1 | _ => VERBOSE = true
        };
    }
    match matches.occurrences_of("regenerate") {
        0 => (),
        1 | _ => {
            // Getting the current exe path
            let mut ce = std::env::current_exe().unwrap();
            ce.pop();
            // Finding the cache file
            let f = ce.join(Path::new("cache.json"));
            remove_file(f)?;
        }
    };

    let mut server = Nickel::new();
    #[allow(unreachable_code)]
    server.get("**", middleware!{ |_req,res|
        // Getting the current exe path
        let mut ce = std::env::current_exe().unwrap();
        ce.pop();
        // Finding the cache file
        let f = ce.join(Path::new("cache.json"));
        let result = get_release(f).unwrap();
        let mut body = "<!DOCTYPE html><html><head><meta charset=\"UTF-8\"></meta><title>BaldmanSagen - Aka Loup Garou</title><style>h1{margin-bottom:0.5rem;font-family:monospace;font-weight:unset;}a{display:block;text-decoration:none;font-size:1.5em;color:black;font-family:monospace;padding:0.5rem 0.7rem;transition:box-shadow 0.25s ease-out,background-color 0.5s ease-out}a:hover{box-shadow:0 1px 3px rgba(0,0,0,0.12),0 1px 2px rgba(0,0,0,0.24);background-color:sandybrown;}a:not(:last-of-type){border-bottom:lightgray solid 1px;}span{font-size:0.85em;color:slategray}span::before{content:\"-\";font-size:1.25rem;}</style></head><body><h1>Téléchargement du serveur</h1>".to_string();
        for release in result.cache {
            body = format!("{}<a href=\"{}\">{}<span>{}</span></a>", body, release.zip, release.name, release.tag);
        }
        body = format!("{}</body></html>", body);
        return res.send(body.as_str());
    });

    server.listen(format!("{}:{}", matches.value_of("address").unwrap_or("127.0.0.1"), matches.value_of("port").unwrap()).as_str())?;

    Ok(())
}